using MultiShop.Catalog.Dtos.ProductDtos;
using MultiShop.Catalog.Entities;
using Riok.Mapperly.Abstractions;

namespace MultiShop.Catalog.Mapping;

[Mapper]
public partial class ProductMapper
{
    [MapperIgnoreTarget(nameof(Product.Id))]
    [MapperIgnoreTarget(nameof(Product.Stock))]
    [MapperIgnoreTarget(nameof(Product.Category))]
    public partial Product ToEntity(CreateProductDto source);

    [MapperIgnoreTarget(nameof(Product.Category))]
    [MapProperty(nameof(UpdateProductDto.ProductId), nameof(Product.Id))]
    [MapProperty(nameof(UpdateProductDto.ProductName), nameof(Product.Name))]
    [MapProperty(nameof(UpdateProductDto.ProductDescription), nameof(Product.Description))]
    [MapProperty(nameof(UpdateProductDto.ProductPrice), nameof(Product.Price))]
    [MapProperty(nameof(UpdateProductDto.ProductImageUrl), nameof(Product.ImageUrl))]
    [MapProperty(nameof(UpdateProductDto.ProductStock), nameof(Product.Stock))]
    public partial Product ToEntity(UpdateProductDto source);

    [MapperIgnoreSource(nameof(Product.Category))]
    [MapProperty(nameof(Product.Id), nameof(GetByIdProductDto.ProductId))]
    [MapProperty(nameof(Product.Name), nameof(GetByIdProductDto.ProductName))]
    [MapProperty(nameof(Product.Description), nameof(GetByIdProductDto.ProductDescription))]
    [MapProperty(nameof(Product.Price), nameof(GetByIdProductDto.ProductPrice))]
    [MapProperty(nameof(Product.ImageUrl), nameof(GetByIdProductDto.ProductImageUrl))]
    [MapProperty(nameof(Product.Stock), nameof(GetByIdProductDto.ProductStock))]
    public partial GetByIdProductDto ToGetByIdDto(Product source);

    [MapperIgnoreSource(nameof(Product.Category))]
    [MapProperty(nameof(Product.Id), nameof(ResultProductDto.ProductId))]
    [MapProperty(nameof(Product.Name), nameof(ResultProductDto.ProductName))]
    [MapProperty(nameof(Product.Description), nameof(ResultProductDto.ProductDescription))]
    [MapProperty(nameof(Product.Price), nameof(ResultProductDto.ProductPrice))]
    [MapProperty(nameof(Product.ImageUrl), nameof(ResultProductDto.ProductImageUrl))]
    [MapProperty(nameof(Product.Stock), nameof(ResultProductDto.ProductStock))]
    public partial ResultProductDto ToResultDto(Product source);
}
