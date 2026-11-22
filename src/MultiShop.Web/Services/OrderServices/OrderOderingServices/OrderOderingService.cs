using MultiShop.DtoLayer.OrderDtos.OrderOrderingDtos;
using System.Net.Http.Json;

namespace MultiShop.Web.Services.OrderServices.OrderOderingServices
{
    public class OrderOderingService : IOrderOderingService
    {
        private readonly HttpClient _httpClient;

        public OrderOderingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ResultOrderingByUserIdDto>> GetOrderingByUserId(string id)
        {
            try
            {
                var responseMessage = await _httpClient.GetAsync("api/Orders");
                if (!responseMessage.IsSuccessStatusCode)
                {
                    return new List<ResultOrderingByUserIdDto>();
                }

                var orders = await responseMessage.Content.ReadFromJsonAsync<List<OrderApiDto>>() ?? new List<OrderApiDto>();
                if (int.TryParse(id, out var customerId))
                {
                    orders = orders.Where(x => x.CustomerId == customerId).ToList();
                }

                return orders.Select(x => new ResultOrderingByUserIdDto
                {
                    OrderingId = x.Id,
                    UserId = x.CustomerId.ToString(),
                    TotalPrice = x.TotalPrice,
                    OrderDate = x.OrderDate.UtcDateTime
                }).ToList();
            }
            catch
            {
                return new List<ResultOrderingByUserIdDto>();
            }
        }

        private sealed record OrderApiDto(
            int Id,
            int CustomerId,
            decimal TotalPrice,
            DateTimeOffset OrderDate);
    }
}
