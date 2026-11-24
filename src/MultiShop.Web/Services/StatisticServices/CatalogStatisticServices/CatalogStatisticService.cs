using MultiShop.DtoLayer.CatalogDtos.ProductDtos;

namespace MultiShop.Web.Services.StatisticServices.CatalogStatisticServices
{
    public class CatalogStatisticService : ICatalogStatisticService
    {
        private readonly HttpClient _httpClient;

        public CatalogStatisticService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<long> GetBrandCount()
        {
            try
            {
                var responseMessage = await _httpClient.GetAsync("api/statistics/GetBrandCount");
                if (!responseMessage.IsSuccessStatusCode) return 0;
                return await responseMessage.Content.ReadFromJsonAsync<long>();
            }
            catch
            {
                return 0;
            }
        }

        public async Task<long> GetCategoryCount()
        {
            try
            {
                var responseMessage = await _httpClient.GetAsync("api/statistics/GetCategoryCount");
                if (!responseMessage.IsSuccessStatusCode) return 0;
                return await responseMessage.Content.ReadFromJsonAsync<long>();
            }
            catch
            {
                return 0;
            }
        }

        public async Task<string> GetMaxPriceProductName()
        {
            try
            {
                var responseMessage = await _httpClient.GetAsync("api/statistics/GetMaxPriceProductName");
                if (!responseMessage.IsSuccessStatusCode) return "-";
                var value = await responseMessage.Content.ReadAsStringAsync();
                return string.IsNullOrWhiteSpace(value) ? "-" : value.Trim('"');
            }
            catch
            {
                return "-";
            }
        }

        public async Task<string> GetMinPriceProductName()
        {
            try
            {
                var responseMessage = await _httpClient.GetAsync("api/statistics/GetMinPriceProductName");
                if (!responseMessage.IsSuccessStatusCode) return "-";
                var value = await responseMessage.Content.ReadAsStringAsync();
                return string.IsNullOrWhiteSpace(value) ? "-" : value.Trim('"');
            }
            catch
            {
                return "-";
            }
        }

        public async Task<decimal> GetProductAvgPrice()
        {
            try
            {
                var responseMessage = await _httpClient.GetAsync("api/statistics/GetProductAvgPrice");
                if (!responseMessage.IsSuccessStatusCode) return 0m;
                return await responseMessage.Content.ReadFromJsonAsync<decimal>();
            }
            catch
            {
                return 0m;
            }
        }

        public async Task<long> GetProductCount()
        {
            try
            {
                var responseMessage = await _httpClient.GetAsync("api/statistics/GetProductCount");
                if (!responseMessage.IsSuccessStatusCode) return 0;
                return await responseMessage.Content.ReadFromJsonAsync<long>();
            }
            catch
            {
                return 0;
            }
        }
    }
}
