using MultiShop.Catalog.Dtos.FeatureDtos;

namespace MultiShop.Catalog.Services.FeatureServices;

public interface IFeatureService
{
    Task<IEnumerable<ResultFeatureDto>> GetAllFeaturesAsync(CancellationToken cancellationToken);
    Task<GetByIdFeatureDto?> GetFeatureByIdAsync(string id, CancellationToken cancellationToken);
    Task<string> CreateFeatureAsync(CreateFeatureDto feature, CancellationToken cancellationToken);
    Task<bool> UpdateFeatureAsync(UpdateFeatureDto feature, CancellationToken cancellationToken);
    Task<bool> DeleteFeatureAsync(string id, CancellationToken cancellationToken);
}
