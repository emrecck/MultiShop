using MultiShop.DtoLayer.IdentityDtos.UserDtos;

namespace MultiShop.Web.Services.UserIdentityServices
{
    public class UserIdentityService : IUserIdentityService
    {
        private readonly HttpClient _httpClient;

        public UserIdentityService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ResultUserDto>> GetAllUserListAsync()
        {
            try
            {
                var responseMessage = await _httpClient.GetAsync("api/users/GetAllUserList");
                if (!responseMessage.IsSuccessStatusCode) return new List<ResultUserDto>();
                return await responseMessage.Content.ReadFromJsonAsync<List<ResultUserDto>>() ?? new List<ResultUserDto>();
            }
            catch
            {
                return new List<ResultUserDto>();
            }
        }
    }
}
