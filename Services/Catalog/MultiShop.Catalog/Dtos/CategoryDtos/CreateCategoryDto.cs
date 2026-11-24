using System;

namespace MultiShop.Catalog.Dtos.CategoryDtos;

public class CreateCategoryDto
{
    public string Name { get; set; } = default!;
    public string? ImageUrl { get; set; }
}
