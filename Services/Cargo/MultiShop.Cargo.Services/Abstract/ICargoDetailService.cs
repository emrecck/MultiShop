using MultiShop.Cargo.Services.Dtos.CargoDetailDtos;

namespace MultiShop.Cargo.Services.Abstract;

public interface ICargoDetailService
{
    Task<IReadOnlyList<ResultCargoDetailDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<GetByIdCargoDetailDto?> GetByIdAsync(int id);
    Task<int> CreateAsync(CreateCargoDetailDto dto);
    Task<bool> UpdateAsync(UpdateCargoDetailDto dto);
    Task DeleteAsync(int id);
}
