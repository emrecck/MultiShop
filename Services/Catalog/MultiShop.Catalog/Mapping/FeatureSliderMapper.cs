using MultiShop.Catalog.Dtos.FeatureSliderDtos;
using MultiShop.Catalog.Entities;
using Riok.Mapperly.Abstractions;

namespace MultiShop.Catalog.Mapping;

[Mapper]
public partial class FeatureSliderMapper
{
    [MapperIgnoreTarget(nameof(FeatureSlider.FeatureSliderId))]
    public partial FeatureSlider ToEntity(CreateFeatureSliderDto source);
    public partial FeatureSlider ToEntity(UpdateFeatureSliderDto source);
    public partial GetByIdFeatureSliderDto ToGetByIdDto(FeatureSlider source);
    public partial ResultFeatureSliderDto ToResultDto(FeatureSlider source);
}
