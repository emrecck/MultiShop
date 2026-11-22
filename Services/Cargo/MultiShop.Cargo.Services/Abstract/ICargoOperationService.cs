using MultiShop.Cargo.Services.Dtos.CargoOperationDtos;

namespace MultiShop.Cargo.Services.Abstract;

public interface ICargoOperationService
{
    Task<IReadOnlyList<ResultCargoOperationDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<GetByIdCargoOperationDto?> GetByIdAsync(int id);
    Task<int> CreateAsync(CreateCargoOperationDto dto);
    Task<bool> UpdateAsync(UpdateCargoOperationDto dto);
    Task DeleteAsync(int id);
}
