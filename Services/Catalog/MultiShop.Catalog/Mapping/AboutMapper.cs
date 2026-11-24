using MultiShop.Catalog.Dtos.AboutDtos;
using MultiShop.Catalog.Entities;
using Riok.Mapperly.Abstractions;

namespace MultiShop.Catalog.Mapping;

[Mapper]
public partial class AboutMapper
{
    [MapperIgnoreTarget(nameof(About.AboutId))]
    public partial About ToEntity(CreateAboutDto source);
    public partial About ToEntity(UpdateAboutDto source);
    public partial GetByIdAboutDto ToGetByIdDto(About source);
    public partial ResultAboutDto ToResultDto(About source);
}
