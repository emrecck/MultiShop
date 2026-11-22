using MultiShop.DtoLayer.CatalogDtos.SpecialOfferDtos;
using Newtonsoft.Json;

namespace MultiShop.Web.Services.CatalogServices.SpecialOfferServices
{
    public class SpecialOfferService : ISpecialOfferService
    {
        private readonly HttpClient _httpClient;
        public SpecialOfferService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task CreateSpecialOfferAsync(CreateSpecialOfferDto createSpecialOfferDto)
        {
            await _httpClient.PostAsJsonAsync("api/special-offers", createSpecialOfferDto);
        }
        public async Task DeleteSpecialOfferAsync(string id)
        {
            await _httpClient.DeleteAsync($"api/special-offers/{id}");
        }
        public async Task<UpdateSpecialOfferDto> GetByIdSpecialOfferAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync($"api/special-offers/{id}");
            if (!responseMessage.IsSuccessStatusCode) return new UpdateSpecialOfferDto();
            return await responseMessage.Content.ReadFromJsonAsync<UpdateSpecialOfferDto>() ?? new UpdateSpecialOfferDto();
        }
        public async Task<List<ResultSpecialOfferDto>> GetAllSpecialOfferAsync()
        {
            try
            {
                var responseMessage = await _httpClient.GetAsync("api/special-offers");
                if (!responseMessage.IsSuccessStatusCode) return new List<ResultSpecialOfferDto>();
                return await responseMessage.Content.ReadFromJsonAsync<List<ResultSpecialOfferDto>>() ?? new List<ResultSpecialOfferDto>();
            }
            catch
            {
                return new List<ResultSpecialOfferDto>();
            }
        }
        public async Task UpdateSpecialOfferAsync(UpdateSpecialOfferDto updateSpecialOfferDto)
        {
            await _httpClient.PutAsJsonAsync($"api/special-offers/{updateSpecialOfferDto.SpecialOfferId}", updateSpecialOfferDto);
        }
    }
}
