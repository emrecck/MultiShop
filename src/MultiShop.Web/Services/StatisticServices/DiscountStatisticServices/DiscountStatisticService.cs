namespace MultiShop.Web.Services.StatisticServices.DiscountStatisticServices
{
    public class DiscountStatisticService : IDiscountStatisticService
    {
        private readonly HttpClient _httpClient;

        public DiscountStatisticService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> GetDiscountCouponCount()
        {
            try
            {
                var responseMessage = await _httpClient.GetAsync("api/coupons/count");
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
