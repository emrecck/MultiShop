using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;

namespace MultiShop.Web.Services.CatalogServices.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateProductAsync(CreateProductDto createProductDto)
        {
            await _httpClient.PostAsJsonAsync("api/products", new
            {
                Name = createProductDto.ProductName,
                Price = createProductDto.ProductPrice,
                Description = createProductDto.ProductDescription,
                ImageUrl = createProductDto.ProductImageUrl,
                CategoryId = createProductDto.CategoryId
            });
        }

        public async Task DeleteProductAsync(string id)
        {
            await _httpClient.DeleteAsync($"api/products/{id}");
        }

        public async Task<UpdateProductDto> GetByIdProductAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync($"api/products/{id}");
            if (!responseMessage.IsSuccessStatusCode)
            {
                return new UpdateProductDto();
            }

            return await responseMessage.Content.ReadFromJsonAsync<UpdateProductDto>() ?? new UpdateProductDto();
        }

        public async Task<List<ResultProductDto>> GetAllProductAsync()
        {
            try
            {
                var responseMessage = await _httpClient.GetAsync("api/products");
                if (!responseMessage.IsSuccessStatusCode)
                {
                    return new List<ResultProductDto>();
                }

                return await responseMessage.Content.ReadFromJsonAsync<List<ResultProductDto>>() ?? new List<ResultProductDto>();
            }
            catch
            {
                return new List<ResultProductDto>();
            }
        }

        public async Task UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            await _httpClient.PutAsJsonAsync($"api/products/{updateProductDto.ProductId}", new
            {
                updateProductDto.ProductId,
                updateProductDto.ProductName,
                updateProductDto.ProductDescription,
                updateProductDto.ProductPrice,
                updateProductDto.ProductImageUrl,
                ProductStock = 0,
                updateProductDto.CategoryId
            });
        }

        public async Task<List<ResultProductWithCategoryDto>> GetProductsWithCategoryAsync()
        {
            try
            {
                var products = await GetAllProductAsync();
                var categoriesResponse = await _httpClient.GetAsync("api/categories");
                var categoryMap = new Dictionary<string, ResultCategoryDto>();

                if (categoriesResponse.IsSuccessStatusCode)
                {
                    var categories = await categoriesResponse.Content.ReadFromJsonAsync<List<CatalogCategoryApiDto>>();
                    if (categories != null)
                    {
                        foreach (var c in categories)
                        {
                            categoryMap[c.Id] = new ResultCategoryDto
                            {
                                CategoryID = c.Id,
                                CategoryName = c.Name,
                                ImageUrl = string.Empty
                            };
                        }
                    }
                }

                return products.Select(p => new ResultProductWithCategoryDto
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    ProductPrice = p.ProductPrice,
                    ProductImageUrl = p.ProductImageUrl,
                    ProductDescription = p.ProductDescription,
                    CategoryId = p.CategoryId,
                    Category = categoryMap.TryGetValue(p.CategoryId ?? string.Empty, out var cat)
                        ? cat
                        : new ResultCategoryDto { CategoryID = p.CategoryId, CategoryName = string.Empty, ImageUrl = string.Empty }
                }).ToList();
            }
            catch
            {
                return new List<ResultProductWithCategoryDto>();
            }
        }

        public async Task<List<ResultProductWithCategoryDto>> GetProductsWithCategoryByCatetegoryIdAsync(string CategoryId)
        {
            if (string.IsNullOrWhiteSpace(CategoryId))
            {
                return await GetProductsWithCategoryAsync();
            }

            try
            {
                var responseMessage = await _httpClient.GetAsync($"api/products/by-category/{CategoryId}");
                if (!responseMessage.IsSuccessStatusCode)
                {
                    return new List<ResultProductWithCategoryDto>();
                }

                var products = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductDto>>() ?? new List<ResultProductDto>();
                var categoriesResponse = await _httpClient.GetAsync("api/categories");
                var categoryMap = new Dictionary<string, ResultCategoryDto>();

                if (categoriesResponse.IsSuccessStatusCode)
                {
                    var categories = await categoriesResponse.Content.ReadFromJsonAsync<List<CatalogCategoryApiDto>>();
                    if (categories != null)
                    {
                        foreach (var c in categories)
                        {
                            categoryMap[c.Id] = new ResultCategoryDto
                            {
                                CategoryID = c.Id,
                                CategoryName = c.Name,
                                ImageUrl = string.Empty
                            };
                        }
                    }
                }

                return products.Select(p => new ResultProductWithCategoryDto
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    ProductPrice = p.ProductPrice,
                    ProductImageUrl = p.ProductImageUrl,
                    ProductDescription = p.ProductDescription,
                    CategoryId = p.CategoryId,
                    Category = categoryMap.TryGetValue(p.CategoryId ?? string.Empty, out var cat)
                        ? cat
                        : new ResultCategoryDto { CategoryID = p.CategoryId, CategoryName = string.Empty, ImageUrl = string.Empty }
                }).ToList();
            }
            catch
            {
                return new List<ResultProductWithCategoryDto>();
            }
        }

        private sealed class CatalogCategoryApiDto
        {
            public string Id { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;
        }
    }
}