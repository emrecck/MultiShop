using MultiShop.Catalog.Dtos.ProductDetailDtos;
using MultiShop.Catalog.Entities;
using Riok.Mapperly.Abstractions;

namespace MultiShop.Catalog.Mapping;

[Mapper]
public partial class ProductDetailMapper
{
    [MapProperty(nameof(CreateProductDetailDto.ProductDetailId), nameof(ProductDetail.Id))]
    [MapProperty(nameof(CreateProductDetailDto.ProductDetailDescription), nameof(ProductDetail.Description))]
    [MapProperty(nameof(CreateProductDetailDto.ProductDetailInfo), nameof(ProductDetail.Information))]
    public partial ProductDetail ToEntity(CreateProductDetailDto source);

    [MapProperty(nameof(UpdateProductDetailDto.ProductDetailId), nameof(ProductDetail.Id))]
    [MapProperty(nameof(UpdateProductDetailDto.ProductDetailDescription), nameof(ProductDetail.Description))]
    [MapProperty(nameof(UpdateProductDetailDto.ProductDetailInfo), nameof(ProductDetail.Information))]
    public partial ProductDetail ToEntity(UpdateProductDetailDto source);

    [MapProperty(nameof(ProductDetail.Id), nameof(GetByIdProductDetailDto.ProductDetailId))]
    [MapProperty(nameof(ProductDetail.Description), nameof(GetByIdProductDetailDto.ProductDetailDescription))]
    [MapProperty(nameof(ProductDetail.Information), nameof(GetByIdProductDetailDto.ProductDetailInfo))]
    public partial GetByIdProductDetailDto ToGetByIdDto(ProductDetail source);

    [MapProperty(nameof(ProductDetail.Id), nameof(ResultProductDetailDto.ProductDetailId))]
    [MapProperty(nameof(ProductDetail.Description), nameof(ResultProductDetailDto.ProductDetailDescription))]
    [MapProperty(nameof(ProductDetail.Information), nameof(ResultProductDetailDto.ProductDetailInfo))]
    public partial ResultProductDetailDto ToResultDto(ProductDetail source);
}
