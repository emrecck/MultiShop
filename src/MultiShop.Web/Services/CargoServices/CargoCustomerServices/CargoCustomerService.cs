using MultiShop.DtoLayer.CargoDtos.CargoCustomerDtos;

namespace MultiShop.Web.Services.CargoServices.CargoCustomerServices
{
    public class CargoCustomerService : ICargoCustomerService
    {
        private readonly HttpClient _httpClient;

        public CargoCustomerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GetCargoCustomerByIdDto> GetByIdCargoCustomerInfoAsync(string id)
        {
            try
            {
                if (int.TryParse(id, out var customerId))
                {
                    var singleResponse = await _httpClient.GetAsync($"api/cargo-receiver-customers/{customerId}");
                    if (singleResponse.IsSuccessStatusCode)
                    {
                        var single = await singleResponse.Content.ReadFromJsonAsync<CargoReceiverCustomerApiDto>();
                        if (single != null)
                        {
                            return Map(single, id);
                        }
                    }
                }

                var listResponse = await _httpClient.GetAsync("api/cargo-receiver-customers");
                if (!listResponse.IsSuccessStatusCode)
                {
                    return new GetCargoCustomerByIdDto();
                }

                var list = await listResponse.Content.ReadFromJsonAsync<List<CargoReceiverCustomerApiDto>>();
                var match = list?.FirstOrDefault(x =>
                    string.Equals(x.Email, id, StringComparison.OrdinalIgnoreCase) ||
                    x.Id.ToString() == id) ?? list?.FirstOrDefault();

                return match != null ? Map(match, id) : new GetCargoCustomerByIdDto();
            }
            catch
            {
                return new GetCargoCustomerByIdDto();
            }
        }

        private static GetCargoCustomerByIdDto Map(CargoReceiverCustomerApiDto source, string userCustomerId) => new()
        {
            cargoCustomerId = source.Id,
            name = source.Name,
            surname = string.Empty,
            email = source.Email,
            phone = source.PhoneNumber,
            district = source.District ?? string.Empty,
            city = source.City ?? string.Empty,
            address = source.Address ?? string.Empty,
            userCustomerId = userCustomerId
        };

        private sealed class CargoReceiverCustomerApiDto
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string PhoneNumber { get; set; } = string.Empty;
            public string? District { get; set; }
            public string? City { get; set; }
            public string? Address { get; set; }
        }
    }
}
