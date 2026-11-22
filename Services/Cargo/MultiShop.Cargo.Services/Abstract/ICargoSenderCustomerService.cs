using MultiShop.Cargo.Services.Dtos.CargoSenderCustomerDtos;

namespace MultiShop.Cargo.Services.Abstract;

public interface ICargoSenderCustomerService
{
    Task<IReadOnlyList<ResultCargoSenderCustomerDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<GetByIdCargoSenderCustomerDto?> GetByIdAsync(int id);
    Task<int> CreateAsync(CreateCargoSenderCustomerDto dto);
    Task<bool> UpdateAsync(UpdateCargoSenderCustomerDto dto);
    Task DeleteAsync(int id);
}
