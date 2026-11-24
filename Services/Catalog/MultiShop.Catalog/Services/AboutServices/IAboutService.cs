using MultiShop.Catalog.Dtos.AboutDtos;

namespace MultiShop.Catalog.Services.AboutServices;

public interface IAboutService
{
    Task<IEnumerable<ResultAboutDto>> GetAllAboutsAsync(CancellationToken cancellationToken);
    Task<GetByIdAboutDto?> GetAboutByIdAsync(string id, CancellationToken cancellationToken);
    Task<string> CreateAboutAsync(CreateAboutDto about, CancellationToken cancellationToken);
    Task<bool> UpdateAboutAsync(UpdateAboutDto about, CancellationToken cancellationToken);
    Task<bool> DeleteAboutAsync(string id, CancellationToken cancellationToken);
}
