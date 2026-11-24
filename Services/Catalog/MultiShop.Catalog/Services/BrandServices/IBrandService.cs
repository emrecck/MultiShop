using MultiShop.Catalog.Dtos.BrandDtos;

namespace MultiShop.Catalog.Services.BrandServices;

public interface IBrandService
{
    Task<IEnumerable<ResultBrandDto>> GetAllBrandsAsync(CancellationToken cancellationToken);
    Task<GetByIdBrandDto?> GetBrandByIdAsync(string id, CancellationToken cancellationToken);
    Task<string> CreateBrandAsync(CreateBrandDto brand, CancellationToken cancellationToken);
    Task<bool> UpdateBrandAsync(UpdateBrandDto brand, CancellationToken cancellationToken);
    Task<bool> DeleteBrandAsync(string id, CancellationToken cancellationToken);
}
