using MultiShop.DtoLayer.CatalogDtos.AboutDtos;
using Newtonsoft.Json;

namespace MultiShop.Web.Services.CatalogServices.AboutServices
{
    public class AboutService : IAboutService
    {
        private readonly HttpClient _httpClient;
        public AboutService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task CreateAboutAsync(CreateAboutDto createAboutDto)
        {
            await _httpClient.PostAsJsonAsync("api/abouts", createAboutDto);
        }
        public async Task DeleteAboutAsync(string id)
        {
            await _httpClient.DeleteAsync($"api/abouts/{id}");
        }
        public async Task<UpdateAboutDto> GetByIdAboutAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync($"api/abouts/{id}");
            if (!responseMessage.IsSuccessStatusCode) return new UpdateAboutDto();
            return await responseMessage.Content.ReadFromJsonAsync<UpdateAboutDto>() ?? new UpdateAboutDto();
        }
        public async Task<List<ResultAboutDto>> GetAllAboutAsync()
        {
            try
            {
                var responseMessage = await _httpClient.GetAsync("api/abouts");
                if (!responseMessage.IsSuccessStatusCode) return new List<ResultAboutDto>();
                return await responseMessage.Content.ReadFromJsonAsync<List<ResultAboutDto>>() ?? new List<ResultAboutDto>();
            }
            catch
            {
                return new List<ResultAboutDto>();
            }
        }
        public async Task UpdateAboutAsync(UpdateAboutDto updateAboutDto)
        {
            await _httpClient.PutAsJsonAsync($"api/abouts/{updateAboutDto.AboutId}", updateAboutDto);
        }
    }
}
