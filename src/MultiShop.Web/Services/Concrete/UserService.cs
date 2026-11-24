using MultiShop.Web.Models;
using MultiShop.Web.Services.Interfaces;

namespace MultiShop.Web.Services.Concrete
{
    public class UserService(HttpClient httpClient) : IUserService
    {
        public async Task<UserDetailViewModel> GetUserInfo()
        {
            try
            {
                var response = await httpClient.GetAsync("api/users/getuser");
                if (response.IsSuccessStatusCode)
                {
                    var info = await response.Content.ReadFromJsonAsync<UserDetailViewModel>();
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
