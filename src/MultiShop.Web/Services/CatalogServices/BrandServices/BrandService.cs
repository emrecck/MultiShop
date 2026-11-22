using MultiShop.DtoLayer.CatalogDtos.BrandDtos;
using Newtonsoft.Json;

namespace MultiShop.Web.Services.CatalogServices.BrandServices
{
    public class BrandService : IBrandService
    {
        private readonly HttpClient _httpClient;
        public BrandService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task CreateBrandAsync(CreateBrandDto createBrandDto)
        {
            await _httpClient.PostAsJsonAsync("api/brands", createBrandDto);
        }
        public async Task DeleteBrandAsync(string id)
        {
            await _httpClient.DeleteAsync($"api/brands/{id}");
        }
        public async Task<UpdateBrandDto> GetByIdBrandAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync($"api/brands/{id}");
            if (!responseMessage.IsSuccessStatusCode) return new UpdateBrandDto();
            return await responseMessage.Content.ReadFromJsonAsync<UpdateBrandDto>() ?? new UpdateBrandDto();
        }
        public async Task<List<ResultBrandDto>> GetAllBrandAsync()
        {
            try
            {
                var responseMessage = await _httpClient.GetAsync("api/brands");
                if (!responseMessage.IsSuccessStatusCode) return new List<ResultBrandDto>();
                return await responseMessage.Content.ReadFromJsonAsync<List<ResultBrandDto>>() ?? new List<ResultBrandDto>();
            }
            catch
            {
                return new List<ResultBrandDto>();
            }
        }
        public async Task UpdateBrandAsync(UpdateBrandDto updateBrandDto)
        {
            await _httpClient.PutAsJsonAsync($"api/brands/{updateBrandDto.BrandId}", updateBrandDto);
        }
    }
}
