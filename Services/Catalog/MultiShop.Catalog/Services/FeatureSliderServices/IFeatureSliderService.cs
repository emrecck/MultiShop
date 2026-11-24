using MultiShop.Catalog.Dtos.FeatureSliderDtos;

namespace MultiShop.Catalog.Services.FeatureSliderServices;

public interface IFeatureSliderService
{
    Task<IEnumerable<ResultFeatureSliderDto>> GetAllFeatureSlidersAsync(CancellationToken cancellationToken);
    Task<GetByIdFeatureSliderDto?> GetFeatureSliderByIdAsync(string id, CancellationToken cancellationToken);
    Task<string> CreateFeatureSliderAsync(CreateFeatureSliderDto featureSlider, CancellationToken cancellationToken);
    Task<bool> UpdateFeatureSliderAsync(UpdateFeatureSliderDto featureSlider, CancellationToken cancellationToken);
    Task<bool> DeleteFeatureSliderAsync(string id, CancellationToken cancellationToken);
    Task<bool> ChangeStatusAsync(string id, bool status, CancellationToken cancellationToken);
}
