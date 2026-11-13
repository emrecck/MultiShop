using System;

namespace MultiShop.Catalog.Dtos.ProductDtos;

public class CreateProductDto
{
    public string Name { get; set; } = default!;
    public decimal Price { get; set; }
    public string Description { get; set; } = default!;
    public string ImageUrl { get; set; } = default!;
    public string CategoryId { get; set; } = default!;
}
