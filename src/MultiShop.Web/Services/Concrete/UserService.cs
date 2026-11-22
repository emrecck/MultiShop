using MultiShop.Web.Models;
using MultiShop.Web.Services.Interfaces;

namespace MultiShop.Web.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserDetailViewModel> GetUserInfo()
        {
            try
            {
                var response = await _httpClient.GetAsync("connect/userinfo");
                if (response.IsSuccessStatusCode)
                {
                    var info = await response.Content.ReadFromJsonAsync<UserDetailViewModel>();
                    if (info != null)
                    {
                        return info;
                    }
                }

                var fallback = await _httpClient.GetAsync("api/users/getuser");
                if (fallback.IsSuccessStatusCode)
                {
                    var info = await fallback.Content.ReadFromJsonAsync<UserDetailViewModel>();
                    if (info != null)
                    {
                        return info;
                    }
                }
            }
            catch
            {
                // IdentityServer erişilemediğinde varsayılan boş kullanıcı dön
            }

            return new UserDetailViewModel
            {
                Id = string.Empty,
                Username = string.Empty,
                Email = string.Empty,
                Name = string.Empty,
                Surname = string.Empty
            };
        }
    }
}
