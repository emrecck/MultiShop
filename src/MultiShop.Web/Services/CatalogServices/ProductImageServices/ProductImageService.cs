using MultiShop.DtoLayer.CatalogDtos.ProductImageDtos;

namespace MultiShop.Web.Services.CatalogServices.ProductImageServices
{
    public class ProductImageService : IProductImageService
    {
        private readonly HttpClient _httpClient;

        public ProductImageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateProductImageAsync(CreateProductImageDto createProductImageDto)
        {
            await _httpClient.PostAsJsonAsync("api/product-images", new
            {
                createProductImageDto.ProductId,
                createProductImageDto.Image1,
                createProductImageDto.Image2,
                createProductImageDto.Image3
            });
        }

        public async Task DeleteProductImageAsync(string id)
        {
            await _httpClient.DeleteAsync($"api/product-images/{id}");
        }

        public async Task<GetByIdProductImageDto> GetByIdProductImageAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync($"api/product-images/{id}");
            if (!responseMessage.IsSuccessStatusCode)
            {
                return new GetByIdProductImageDto();
            }

            var value = await responseMessage.Content.ReadFromJsonAsync<CatalogProductImageApiDto>();
            return value == null ? new GetByIdProductImageDto() : MapToGetById(value);
        }

        public async Task<List<ResultProductImageDto>> GetAllProductImageAsync()
        {
            try
            {
                var responseMessage = await _httpClient.GetAsync("api/product-images");
                if (!responseMessage.IsSuccessStatusCode)
                {
                    return new List<ResultProductImageDto>();
                }

                var values = await responseMessage.Content.ReadFromJsonAsync<List<CatalogProductImageApiDto>>();
                return values?.Select(x => new ResultProductImageDto
                {
                    ProductImageID = x.Id,
                    Image1 = x.Image1,
                    Image2 = x.Image2,
                    Image3 = x.Image3,
                    Image4 = string.Empty,
                    ProductId = x.ProductId
                }).ToList() ?? new List<ResultProductImageDto>();
            }
            catch
            {
                return new List<ResultProductImageDto>();
            }
        }

        public async Task UpdateProductImageAsync(UpdateProductImageDto updateProductImageDto)
        {
            await _httpClient.PutAsJsonAsync($"api/product-images/{updateProductImageDto.ProductImageID}", new
            {
                Id = updateProductImageDto.ProductImageID,
                updateProductImageDto.Image1,
                updateProductImageDto.Image2,
                updateProductImageDto.Image3,
                updateProductImageDto.ProductId
            });
        }

        public async Task<GetByIdProductImageDto> GetByProductIdProductImageAsync(string id)
        {
            try
            {
                var responseMessage = await _httpClient.GetAsync($"api/product-images/by-product/{id}");
                if (!responseMessage.IsSuccessStatusCode)
                {
                    return new GetByIdProductImageDto { ProductId = id };
                }

                var value = await responseMessage.Content.ReadFromJsonAsync<CatalogProductImageApiDto>();
                return value == null ? new GetByIdProductImageDto { ProductId = id } : MapToGetById(value);
            }
            catch
            {
                return new GetByIdProductImageDto { ProductId = id };
            }
        }

        private static GetByIdProductImageDto MapToGetById(CatalogProductImageApiDto value) => new()
        {
            ProductImageID = value.Id,
            Image1 = value.Image1,
            Image2 = value.Image2,
            Image3 = value.Image3,
            Image4 = string.Empty,
            ProductId = value.ProductId
        };

        private sealed class CatalogProductImageApiDto
        {
            public string Id { get; set; } = string.Empty;
            public string Image1 { get; set; } = string.Empty;
            public string Image2 { get; set; } = string.Empty;
            public string Image3 { get; set; } = string.Empty;
            public string ProductId { get; set; } = string.Empty;
        }
    }
}
