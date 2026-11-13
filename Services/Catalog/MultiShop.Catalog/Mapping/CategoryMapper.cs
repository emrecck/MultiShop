using MultiShop.Catalog.Dtos.CategoryDtos;
using MultiShop.Catalog.Entities;
using Riok.Mapperly.Abstractions;

namespace MultiShop.Catalog.Mapping;

[Mapper]
public partial class CategoryMapper
{
    [MapperIgnoreTarget(nameof(Category.Id))]
    public partial Category ToEntity(CreateCategoryDto source);

    public partial Category ToEntity(UpdateCategoryDto source);

    public partial GetByIdCategoryDto ToGetByIdDto(Category source);

    public partial ResultCategoryDto ToResultDto(Category source);
}
