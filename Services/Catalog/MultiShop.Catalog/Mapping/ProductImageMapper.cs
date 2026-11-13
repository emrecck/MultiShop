using MultiShop.Catalog.Dtos.ProductImageDtos;
using MultiShop.Catalog.Entities;
using Riok.Mapperly.Abstractions;

namespace MultiShop.Catalog.Mapping;

[Mapper]
public partial class ProductImageMapper
{
    [MapperIgnoreTarget(nameof(ProductImage.Id))]
    [MapperIgnoreTarget(nameof(ProductImage.Product))]
    public partial ProductImage ToEntity(CreateProductImageDto source);

    [MapperIgnoreTarget(nameof(ProductImage.Product))]
    public partial ProductImage ToEntity(UpdateProductImageDto source);

    [MapperIgnoreSource(nameof(ProductImage.Product))]
    public partial GetByIdProductImageDto ToGetByIdDto(ProductImage source);

    [MapperIgnoreSource(nameof(ProductImage.Product))]
    public partial ResultProductImageDto ToResultDto(ProductImage source);
}
