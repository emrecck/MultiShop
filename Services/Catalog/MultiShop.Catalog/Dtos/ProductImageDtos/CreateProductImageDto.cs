using System;

namespace MultiShop.Catalog.Dtos.ProductImageDtos;

public class CreateProductImageDto
{
    public string ProductId { get; set; } = default!;
    public string Image1 { get; set; } = default!;
    public string Image2 { get; set; } = default!;
    public string Image3 { get; set; } = default!;
}
