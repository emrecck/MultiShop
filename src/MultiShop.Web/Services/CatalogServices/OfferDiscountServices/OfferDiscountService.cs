using MultiShop.DtoLayer.CatalogDtos.OfferDiscountDtos;

namespace MultiShop.Web.Services.CatalogServices.OfferDiscountServices
{
    public class OfferDiscountService : IOfferDiscountService
    {
        private readonly HttpClient _httpClient;
        public OfferDiscountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task CreateOfferDiscountAsync(CreateOfferDiscountDto createOfferDiscountDto)
        {
            await _httpClient.PostAsJsonAsync("api/offer-discounts", createOfferDiscountDto);
        }
        public async Task DeleteOfferDiscountAsync(string id)
        {
            await _httpClient.DeleteAsync($"api/offer-discounts/{id}");
        }
        public async Task<UpdateOfferDiscountDto> GetByIdOfferDiscountAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync($"api/offer-discounts/{id}");
            if (!responseMessage.IsSuccessStatusCode) return new UpdateOfferDiscountDto();
            return await responseMessage.Content.ReadFromJsonAsync<UpdateOfferDiscountDto>() ?? new UpdateOfferDiscountDto();
        }
        public async Task<List<ResultOfferDiscountDto>> GetAllOfferDiscountAsync()
        {
            try
            {
                var responseMessage = await _httpClient.GetAsync("api/offer-discounts");
                if (!responseMessage.IsSuccessStatusCode) return new List<ResultOfferDiscountDto>();
                return await responseMessage.Content.ReadFromJsonAsync<List<ResultOfferDiscountDto>>() ?? new List<ResultOfferDiscountDto>();
            }
            catch
            {
                return new List<ResultOfferDiscountDto>();
            }
        }
        public async Task UpdateOfferDiscountAsync(UpdateOfferDiscountDto updateOfferDiscountDto)
        {
            await _httpClient.PutAsJsonAsync($"api/offer-discounts/{updateOfferDiscountDto.OfferDiscountId}", updateOfferDiscountDto);
        }
    }
}
