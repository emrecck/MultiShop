using MultiShop.DtoLayer.IdentityDtos.UserDtos;
using Newtonsoft.Json;

namespace MultiShop.Web.Services.StatisticServices.UserStatisticServices
{
    public class UserStatisticService : IUserStatisticService
    {
        private readonly HttpClient _httpClient;

        public UserStatisticService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> GetUsercount()
        {
            try
            {
                var responseMessage = await _httpClient.GetAsync("api/Statistics");
                if (!responseMessage.IsSuccessStatusCode) return 0;
                return await responseMessage.Content.ReadFromJsonAsync<int>();
            }
            catch
            {
                return 0;
            }
        }
    }
}
