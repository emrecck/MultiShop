using MultiShop.DtoLayer.CatalogDtos.FeatureDtos;
using Newtonsoft.Json;

namespace MultiShop.Web.Services.CatalogServices.FeatureServices
{
    public class FeatureService : IFeatureService
    {
        private readonly HttpClient _httpClient;
        public FeatureService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task CreateFeatureAsync(CreateFeatureDto createFeatureDto)
        {
            await _httpClient.PostAsJsonAsync("api/features", createFeatureDto);
        }
        public async Task DeleteFeatureAsync(string id)
        {
            await _httpClient.DeleteAsync($"api/features/{id}");
        }
        public async Task<UpdateFeatureDto> GetByIdFeatureAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync($"api/features/{id}");
            if (!responseMessage.IsSuccessStatusCode) return new UpdateFeatureDto();
            return await responseMessage.Content.ReadFromJsonAsync<UpdateFeatureDto>() ?? new UpdateFeatureDto();
        }
        public async Task<List<ResultFeatureDto>> GetAllFeatureAsync()
        {
            try
            {
                var responseMessage = await _httpClient.GetAsync("api/features");
                if (!responseMessage.IsSuccessStatusCode) return new List<ResultFeatureDto>();
                return await responseMessage.Content.ReadFromJsonAsync<List<ResultFeatureDto>>() ?? new List<ResultFeatureDto>();
            }
            catch
            {
                return new List<ResultFeatureDto>();
            }
        }
        public async Task UpdateFeatureAsync(UpdateFeatureDto updateFeatureDto)
        {
            await _httpClient.PutAsJsonAsync($"api/features/{updateFeatureDto.FeatureId}", updateFeatureDto);
        }
    }
}
