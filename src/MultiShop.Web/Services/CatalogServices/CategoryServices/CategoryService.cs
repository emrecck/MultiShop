using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;

namespace MultiShop.Web.Services.CatalogServices.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            await _httpClient.PostAsJsonAsync("api/categories", new
            {
                Name = createCategoryDto.CategoryName,
                ImageUrl = createCategoryDto.ImageUrl
            });
        }

        public async Task DeleteCategoryAsync(string id)
        {
            await _httpClient.DeleteAsync($"api/categories/{id}");
        }

        public async Task<UpdateCategoryDto> GetByIdCategoryAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync($"api/categories/{id}");
            if (!responseMessage.IsSuccessStatusCode)
            {
                return new UpdateCategoryDto();
            }

            var value = await responseMessage.Content.ReadFromJsonAsync<CatalogCategoryApiDto>();
            if (value == null)
            {
                return new UpdateCategoryDto();
            }

            return new UpdateCategoryDto
            {
                CategoryID = value.Id,
                CategoryName = value.Name,
                ImageUrl = value.ImageUrl ?? string.Empty
            };
        }

        public async Task<List<ResultCategoryDto>> GetAllCategoryAsync()
        {
            try
            {
                var responseMessage = await _httpClient.GetAsync("api/categories");
                if (!responseMessage.IsSuccessStatusCode)
                {
                    return new List<ResultCategoryDto>();
                }

                var values = await responseMessage.Content.ReadFromJsonAsync<List<CatalogCategoryApiDto>>();
                return values?.Select(x => new ResultCategoryDto
                {
                    CategoryID = x.Id,
                    CategoryName = x.Name,
                    ImageUrl = x.ImageUrl ?? string.Empty
                }).ToList() ?? new List<ResultCategoryDto>();
            }
            catch
            {
                return new List<ResultCategoryDto>();
            }
        }

        public async Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto)
        {
            await _httpClient.PutAsJsonAsync($"api/categories/{updateCategoryDto.CategoryID}", new
            {
                Id = updateCategoryDto.CategoryID,
                Name = updateCategoryDto.CategoryName,
                ImageUrl = updateCategoryDto.ImageUrl
            });
        }

        private sealed class CatalogCategoryApiDto
        {
            public string Id { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;
            public string? ImageUrl { get; set; }
        }
    }
}
