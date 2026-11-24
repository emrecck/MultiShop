using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using MultiShop.Web.Services.Interfaces;
using System.Net;
using System.Net.Http.Headers;

namespace MultiShop.Web.Handlers
{
    public class ResourceOwnerPasswordTokenHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IIdentityService _identityService;

        public ResourceOwnerPasswordTokenHandler(IHttpContextAccessor httpContextAccessor, IIdentityService identityService)
        {
            _httpContextAccessor = httpContextAccessor;
            _identityService = identityService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var accessToken = httpContext != null
                ? await httpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken)
                : null;

            if (!string.IsNullOrEmpty(accessToken))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }

            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized && httpContext != null)
            {
                var refreshed = await _identityService.GetRefreshToken();
                if (refreshed)
                {
                    var newAccessToken = await httpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
                    if (!string.IsNullOrEmpty(newAccessToken))
                    {
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", newAccessToken);
                        response = await base.SendAsync(request, cancellationToken);
                    }
                }
            }

            return response;
        }
    }
}
