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
                var responseMessage = await _httpClient.GetAsync("api/coupons");
                if (!responseMessage.IsSuccessStatusCode) return 0;
                var coupons = await responseMessage.Content.ReadFromJsonAsync<List<object>>();
                return coupons?.Count ?? 0;
            }
            catch
            {
                return 0;
            }
        }
    }
}
