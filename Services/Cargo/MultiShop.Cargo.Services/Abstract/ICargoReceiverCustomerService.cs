using MultiShop.Cargo.Services.Dtos.CargoReceiverCustomerDtos;

namespace MultiShop.Cargo.Services.Abstract;

public interface ICargoReceiverCustomerService
{
    Task<IReadOnlyList<ResultCargoReceiverCustomerDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<GetByIdCargoReceiverCustomerDto?> GetByIdAsync(int id);
    Task<int> CreateAsync(CreateCargoReceiverCustomerDto dto);
    Task<bool> UpdateAsync(UpdateCargoReceiverCustomerDto dto);
    Task DeleteAsync(int id);
}
