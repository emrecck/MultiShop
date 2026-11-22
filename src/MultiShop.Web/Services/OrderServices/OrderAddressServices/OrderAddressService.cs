using MultiShop.DtoLayer.OrderDtos.OrderAddressDtos;

namespace MultiShop.Web.Services.OrderServices.OrderAddressServices
{
    public class OrderAddressService : IOrderAddressService
    {
        private readonly HttpClient _httpClient;

        public OrderAddressService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateOrderAddressAsync(CreateOrderAddressDto createOrderAddressDto)
        {
            var street = string.Join(" ", new[] { createOrderAddressDto.Detail1, createOrderAddressDto.Detail2 }
                .Where(s => !string.IsNullOrWhiteSpace(s)));

            await _httpClient.PostAsJsonAsync("api/Adresses", new
            {
                CustomerId = int.TryParse(createOrderAddressDto.UserId, out var customerId) ? customerId : 0,
                Street = street,
                City = createOrderAddressDto.City ?? string.Empty,
                State = createOrderAddressDto.District ?? string.Empty,
                PostalCode = createOrderAddressDto.ZipCode ?? string.Empty,
                Country = createOrderAddressDto.Country ?? string.Empty
            });
        }
    }
}
