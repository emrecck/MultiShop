using MultiShop.DtoLayer.CargoDtos.CargoCompanyDtos;

namespace MultiShop.Web.Services.CargoServices.CargoCompanyServices
{
    public class CargoCompanyService : ICargoCompanyService
    {
        private readonly HttpClient _httpClient;

        public CargoCompanyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateCargoCompanyAsync(CreateCargoCompanyDto createCargoCompanyDto)
        {
            await _httpClient.PostAsJsonAsync("api/cargo-companies", new
            {
                CompanyName = createCargoCompanyDto.cargoCompanyName,
                CompanyEmail = "info@multishop.local",
                CompanyPhoneNumber = "0000000000",
                CompanyAddress = string.Empty
            });
        }

        public async Task DeleteCargoCompanyAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/cargo-companies/{id}");
        }

        public async Task<List<ResultCargoCompanyDto>> GetAllCargoCompanyAsync()
        {
            try
            {
                var responseMessage = await _httpClient.GetAsync("api/cargo-companies");
                if (!responseMessage.IsSuccessStatusCode)
                {
                    return new List<ResultCargoCompanyDto>();
                }

                var values = await responseMessage.Content.ReadFromJsonAsync<List<CargoCompanyApiDto>>();
                return values?.Select(x => new ResultCargoCompanyDto
                {
                    cargoCompanyId = x.Id,
                    cargoCompanyName = x.CompanyName
                }).ToList() ?? new List<ResultCargoCompanyDto>();
            }
            catch
            {
                return new List<ResultCargoCompanyDto>();
            }
        }

        public async Task<UpdateCargoCompanyDto> GetByIdCargoCompanyAsync(int id)
        {
            var responseMessage = await _httpClient.GetAsync($"api/cargo-companies/{id}");
            if (!responseMessage.IsSuccessStatusCode)
            {
                return new UpdateCargoCompanyDto();
            }

            var value = await responseMessage.Content.ReadFromJsonAsync<CargoCompanyApiDto>();
            if (value == null)
            {
                return new UpdateCargoCompanyDto();
            }

            return new UpdateCargoCompanyDto
            {
                cargoCompanyId = value.Id,
                cargoCompanyName = value.CompanyName
            };
        }

        public async Task UpdateCargoCompanyAsync(UpdateCargoCompanyDto updateCargoCompanyDto)
        {
            var existingResponse = await _httpClient.GetAsync($"api/cargo-companies/{updateCargoCompanyDto.cargoCompanyId}");
            CargoCompanyApiDto? existing = null;
            if (existingResponse.IsSuccessStatusCode)
            {
                existing = await existingResponse.Content.ReadFromJsonAsync<CargoCompanyApiDto>();
            }

            await _httpClient.PutAsJsonAsync($"api/cargo-companies/{updateCargoCompanyDto.cargoCompanyId}", new
            {
                Id = updateCargoCompanyDto.cargoCompanyId,
                CompanyName = updateCargoCompanyDto.cargoCompanyName,
                CompanyEmail = existing?.CompanyEmail ?? "info@multishop.local",
                CompanyPhoneNumber = existing?.CompanyPhoneNumber ?? "0000000000",
                CompanyAddress = existing?.CompanyAddress ?? string.Empty,
                IsActive = existing?.IsActive ?? true
            });
        }

        private sealed class CargoCompanyApiDto
        {
            public int Id { get; set; }
            public string CompanyName { get; set; } = string.Empty;
            public string CompanyEmail { get; set; } = string.Empty;
            public string CompanyPhoneNumber { get; set; } = string.Empty;
            public string? CompanyAddress { get; set; }
            public bool IsActive { get; set; } = true;
        }
    }
}
