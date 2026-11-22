using MultiShop.DtoLayer.CatalogDtos.ProductDetailDtos;

namespace MultiShop.Web.Services.CatalogServices.ProductDetailServices
{
    public class ProductDetailService : IProductDetailService
    {
        private readonly HttpClient _httpClient;

        public ProductDetailService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateProductDetailAsync(CreateProductDetailDto createProductDetailDto)
        {
            await _httpClient.PostAsJsonAsync("api/product-details", new
            {
                createProductDetailDto.ProductId,
                ProductDetailDescription = createProductDetailDto.ProductDescription,
                ProductDetailInfo = createProductDetailDto.ProductInfo
            });
        }

        public async Task DeleteProductDetailAsync(string id)
        {
            await _httpClient.DeleteAsync($"api/product-details/{id}");
        }

        public async Task<GetByIdProductDetailDto> GetByIdProductDetailAsync(string id)
        {
            try
            {
                var responseMessage = await _httpClient.GetAsync($"api/product-details/{id}");
                if (!responseMessage.IsSuccessStatusCode)
                {
                    return new GetByIdProductDetailDto();
                }

                var value = await responseMessage.Content.ReadFromJsonAsync<CatalogProductDetailApiDto>();
                return value == null ? new GetByIdProductDetailDto() : MapToGetById(value);
            }
            catch
            {
                return new GetByIdProductDetailDto();
            }
        }

        public async Task<List<ResultProductDetailDto>> GetAllProductDetailAsync()
        {
            try
            {
                var responseMessage = await _httpClient.GetAsync("api/product-details");
                if (!responseMessage.IsSuccessStatusCode)
                {
                    return new List<ResultProductDetailDto>();
                }

                var values = await responseMessage.Content.ReadFromJsonAsync<List<CatalogProductDetailApiDto>>();
                return values?.Select(x => new ResultProductDetailDto
                {
                    ProductDetailId = x.ProductDetailId,
                    ProductDescription = x.ProductDetailDescription,
                    ProductInfo = x.ProductDetailInfo,
                    ProductId = x.ProductId
                }).ToList() ?? new List<ResultProductDetailDto>();
            }
            catch
            {
                return new List<ResultProductDetailDto>();
            }
        }

        public async Task UpdateProductDetailAsync(UpdateProductDetailDto updateProductDetailDto)
        {
            await _httpClient.PutAsJsonAsync($"api/product-details/{updateProductDetailDto.ProductDetailId}", new
            {
                updateProductDetailDto.ProductDetailId,
                ProductDetailDescription = updateProductDetailDto.ProductDescription,
                ProductDetailInfo = updateProductDetailDto.ProductInfo,
                updateProductDetailDto.ProductId
            });
        }

        public async Task<GetByIdProductDetailDto> GetByProductIdProductDetailAsync(string id)
        {
            var allDetails = await GetAllProductDetailAsync();
            var match = allDetails.FirstOrDefault(x => string.Equals(x.ProductId, id, StringComparison.OrdinalIgnoreCase));
            if (match == null)
            {
                return new GetByIdProductDetailDto { ProductId = id };
            }

            return new GetByIdProductDetailDto
            {
                ProductDetailId = match.ProductDetailId,
                ProductDescription = match.ProductDescription,
                ProductInfo = match.ProductInfo,
                ProductId = match.ProductId
            };
        }

        private static GetByIdProductDetailDto MapToGetById(CatalogProductDetailApiDto value) => new()
        {
            ProductDetailId = value.ProductDetailId,
            ProductDescription = value.ProductDetailDescription,
            ProductInfo = value.ProductDetailInfo,
            ProductId = value.ProductId
        };

        private sealed class CatalogProductDetailApiDto
        {
            public string ProductDetailId { get; set; } = string.Empty;
            public string ProductDetailDescription { get; set; } = string.Empty;
            public string ProductDetailInfo { get; set; } = string.Empty;
            public string ProductId { get; set; } = string.Empty;
        }
    }
}
