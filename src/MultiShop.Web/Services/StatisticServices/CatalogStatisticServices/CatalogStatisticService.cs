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
                var responseMessage = await _httpClient.GetAsync("api/brands");
                if (!responseMessage.IsSuccessStatusCode) return 0;
                var items = await responseMessage.Content.ReadFromJsonAsync<List<object>>();
                return items?.Count ?? 0;
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
                var responseMessage = await _httpClient.GetAsync("api/categories");
                if (!responseMessage.IsSuccessStatusCode) return 0;
                var items = await responseMessage.Content.ReadFromJsonAsync<List<object>>();
                return items?.Count ?? 0;
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
                var products = await GetProductsAsync();
                return products.OrderByDescending(x => x.ProductPrice).FirstOrDefault()?.ProductName ?? "-";
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
                var products = await GetProductsAsync();
                return products.OrderBy(x => x.ProductPrice).FirstOrDefault()?.ProductName ?? "-";
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
                var products = await GetProductsAsync();
                return products.Count == 0 ? 0m : products.Average(x => x.ProductPrice);
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
                var products = await GetProductsAsync();
                return products.Count;
            }
            catch
            {
                return 0;
            }
        }

        private async Task<List<ResultProductDto>> GetProductsAsync()
        {
            var responseMessage = await _httpClient.GetAsync("api/products");
            if (!responseMessage.IsSuccessStatusCode) return new List<ResultProductDto>();
            return await responseMessage.Content.ReadFromJsonAsync<List<ResultProductDto>>() ?? new List<ResultProductDto>();
        }
    }
}
