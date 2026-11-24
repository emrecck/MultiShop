using MultiShop.Catalog.Dtos.OfferDiscountDtos;
using MultiShop.Catalog.Entities;
using Riok.Mapperly.Abstractions;

namespace MultiShop.Catalog.Mapping;

[Mapper]
public partial class OfferDiscountMapper
{
    [MapperIgnoreTarget(nameof(OfferDiscount.OfferDiscountId))]
    public partial OfferDiscount ToEntity(CreateOfferDiscountDto source);
    public partial OfferDiscount ToEntity(UpdateOfferDiscountDto source);
    public partial GetByIdOfferDiscountDto ToGetByIdDto(OfferDiscount source);
    public partial ResultOfferDiscountDto ToResultDto(OfferDiscount source);
}
