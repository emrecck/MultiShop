using System.Security.Claims;
using MultiShop.Web.Services.Interfaces;

namespace MultiShop.Web.Services.Concrete
{
    public class LoginService : ILoginService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public LoginService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public string GetUserId =>
            _contextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? _contextAccessor.HttpContext?.User?.FindFirst("sub")?.Value
            ?? string.Empty;
    }
}
