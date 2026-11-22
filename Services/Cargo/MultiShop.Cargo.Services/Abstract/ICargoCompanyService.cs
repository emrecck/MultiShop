using MultiShop.Cargo.Services.Dtos.CargoCompanyDtos;

namespace MultiShop.Cargo.Services.Abstract;

public interface ICargoCompanyService
{
    Task<IReadOnlyList<ResultCargoCompanyDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<GetByIdCargoCompanyDto?> GetByIdAsync(int id);
    Task<int> CreateAsync(CreateCargoCompanyDto dto);
    Task<bool> UpdateAsync(UpdateCargoCompanyDto dto);
    Task DeleteAsync(int id);
}
