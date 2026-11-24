using MultiShop.Catalog.Dtos.BrandDtos;
using MultiShop.Catalog.Entities;
using Riok.Mapperly.Abstractions;

namespace MultiShop.Catalog.Mapping;

[Mapper]
public partial class BrandMapper
{
    [MapperIgnoreTarget(nameof(Brand.BrandId))]
    public partial Brand ToEntity(CreateBrandDto source);
    public partial Brand ToEntity(UpdateBrandDto source);
    public partial GetByIdBrandDto ToGetByIdDto(Brand source);
    public partial ResultBrandDto ToResultDto(Brand source);
}
