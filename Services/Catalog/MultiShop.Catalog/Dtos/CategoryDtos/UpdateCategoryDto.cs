using System;

namespace MultiShop.Catalog.Dtos.CategoryDtos;

public class UpdateCategoryDto
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? ImageUrl { get; set; }
}
