namespace MultiShop.Web.Services.StatisticServices.MessageStatisticServices
{
    public class MessageStatisticService : IMessageStatisticService
    {
        private readonly HttpClient _httpClient;

        public MessageStatisticService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> GetTotalMessageCount()
        {
            try
            {
                var responseMessage = await _httpClient.GetAsync("api/UserMessage/GetTotalMessageCount");
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
