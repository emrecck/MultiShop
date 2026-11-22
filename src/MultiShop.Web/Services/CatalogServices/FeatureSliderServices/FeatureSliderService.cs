using MultiShop.DtoLayer.CatalogDtos.FeatureSliderDtos;
using Newtonsoft.Json;

namespace MultiShop.Web.Services.CatalogServices.FeatureSliderServices
{
    public class FeatureSliderService : IFeatureSliderService
    {
        private readonly HttpClient _httpClient;
        public FeatureSliderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task CreateFeatureSliderAsync(CreateFeatureSliderDto createFeatureSliderDto)
        {
            await _httpClient.PostAsJsonAsync("api/feature-sliders", createFeatureSliderDto);
        }
        public async Task DeleteFeatureSliderAsync(string id)
        {
            await _httpClient.DeleteAsync($"api/feature-sliders/{id}");
        }
        public async Task<UpdateFeatureSliderDto> GetByIdFeatureSliderAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync($"api/feature-sliders/{id}");
            if (!responseMessage.IsSuccessStatusCode) return new UpdateFeatureSliderDto();
            return await responseMessage.Content.ReadFromJsonAsync<UpdateFeatureSliderDto>() ?? new UpdateFeatureSliderDto();
        }
        public async Task<List<ResultFeatureSliderDto>> GetAllFeatureSliderAsync()
        {
            try
            {
                var responseMessage = await _httpClient.GetAsync("api/feature-sliders");
                if (!responseMessage.IsSuccessStatusCode) return new List<ResultFeatureSliderDto>();
                return await responseMessage.Content.ReadFromJsonAsync<List<ResultFeatureSliderDto>>() ?? new List<ResultFeatureSliderDto>();
            }
            catch
            {
                return new List<ResultFeatureSliderDto>();
            }
        }
        public async Task UpdateFeatureSliderAsync(UpdateFeatureSliderDto updateFeatureSliderDto)
        {
            await _httpClient.PutAsJsonAsync($"api/feature-sliders/{updateFeatureSliderDto.FeatureSliderId}", updateFeatureSliderDto);
        }

        public async Task FeatureSliderChageStatusToTrue(string id)
        {
            await _httpClient.GetAsync($"api/feature-sliders/FeatureSliderChangeStatusToTrue/{id}");
        }

        public async Task FeatureSliderChageStatusToFalse(string id)
        {
            await _httpClient.GetAsync($"api/feature-sliders/FeatureSliderChangeStatusToFalse/{id}");
        }
    }
}
