using MultiShop.DtoLayer.DiscountDtos;
using MultiShop.DtoLayer.MessageDtos;

namespace MultiShop.Web.Services.MessageServices
{
    public class MessageService : IMessageService
    {
        private readonly HttpClient _httpClient;

        public MessageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ResultInboxMessageDto>> GetInboxMessageAsync(string id)
        {
            try
            {
                var responseMessage = await _httpClient.GetAsync($"api/UserMessage/GetMessageInbox?id={id}");
                if (!responseMessage.IsSuccessStatusCode) return new List<ResultInboxMessageDto>();
                return await responseMessage.Content.ReadFromJsonAsync<List<ResultInboxMessageDto>>() ?? new List<ResultInboxMessageDto>();
            }
            catch
            {
                return new List<ResultInboxMessageDto>();
            }
        }

        public async Task<List<ResultSendboxMessageDto>> GetSendboxMessageAsync(string id)
        {
            try
            {
                var responseMessage = await _httpClient.GetAsync($"api/UserMessage/GetMessageSendbox?id={id}");
                if (!responseMessage.IsSuccessStatusCode) return new List<ResultSendboxMessageDto>();
                return await responseMessage.Content.ReadFromJsonAsync<List<ResultSendboxMessageDto>>() ?? new List<ResultSendboxMessageDto>();
            }
            catch
            {
                return new List<ResultSendboxMessageDto>();
            }
        }

        public async Task<int> GetTotalMessageCountByReceiverId(string id)
        {
            try
            {
                var responseMessage = await _httpClient.GetAsync($"api/UserMessage/GetTotalMessageCountByReceiverId?id={id}");
                if (!responseMessage.IsSuccessStatusCode) return 0;
                return await responseMessage.Content.ReadFromJsonAsync<int>();
            }
            catch
            {
                return 0;
            }
        }
    }
}