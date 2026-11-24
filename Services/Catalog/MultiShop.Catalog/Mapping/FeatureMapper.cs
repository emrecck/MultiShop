using MultiShop.Catalog.Dtos.FeatureDtos;
using MultiShop.Catalog.Entities;
using Riok.Mapperly.Abstractions;

namespace MultiShop.Catalog.Mapping;

[Mapper]
public partial class FeatureMapper
{
    [MapperIgnoreTarget(nameof(Feature.FeatureId))]
    public partial Feature ToEntity(CreateFeatureDto source);
    public partial Feature ToEntity(UpdateFeatureDto source);
    public partial GetByIdFeatureDto ToGetByIdDto(Feature source);
    public partial ResultFeatureDto ToResultDto(Feature source);
}
