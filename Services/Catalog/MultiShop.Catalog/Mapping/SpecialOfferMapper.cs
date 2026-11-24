using MultiShop.Catalog.Dtos.SpecialOfferDtos;
using MultiShop.Catalog.Entities;
using Riok.Mapperly.Abstractions;

namespace MultiShop.Catalog.Mapping;

[Mapper]
public partial class SpecialOfferMapper
{
    [MapperIgnoreTarget(nameof(SpecialOffer.SpecialOfferId))]
    public partial SpecialOffer ToEntity(CreateSpecialOfferDto source);
    public partial SpecialOffer ToEntity(UpdateSpecialOfferDto source);
    public partial GetByIdSpecialOfferDto ToGetByIdDto(SpecialOffer source);
    public partial ResultSpecialOfferDto ToResultDto(SpecialOffer source);
}
