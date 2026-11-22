using MultiShop.DtoLayer.CatalogDtos.ContactDtos;
using Newtonsoft.Json;

namespace MultiShop.Web.Services.CatalogServices.ContactServices
{
    public class ContactService : IContactService
    {
        private readonly HttpClient _httpClient;
        public ContactService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task CreateContactAsync(CreateContactDto createContactDto)
        {
            await _httpClient.PostAsJsonAsync("api/contacts", createContactDto);
        }
        public async Task DeleteContactAsync(string id)
        {
            await _httpClient.DeleteAsync($"api/contacts/{id}");
        }
        public async Task<GetByIdContactDto> GetByIdContactAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync($"api/contacts/{id}");
            if (!responseMessage.IsSuccessStatusCode) return new GetByIdContactDto();
            return await responseMessage.Content.ReadFromJsonAsync<GetByIdContactDto>() ?? new GetByIdContactDto();
        }
        public async Task<List<ResultContactDto>> GetAllContactAsync()
        {
            try
            {
                var responseMessage = await _httpClient.GetAsync("api/contacts");
                if (!responseMessage.IsSuccessStatusCode) return new List<ResultContactDto>();
                return await responseMessage.Content.ReadFromJsonAsync<List<ResultContactDto>>() ?? new List<ResultContactDto>();
            }
            catch
            {
                return new List<ResultContactDto>();
            }
        }
        public async Task UpdateContactAsync(UpdateContactDto updateContactDto)
        {
            await _httpClient.PutAsJsonAsync($"api/contacts/{updateContactDto.ContactId}", updateContactDto);
        }
    }
}
