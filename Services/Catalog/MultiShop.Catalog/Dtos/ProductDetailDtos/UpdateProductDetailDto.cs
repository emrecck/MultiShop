namespace MultiShop.Catalog.Dtos.ProductDetailDtos
{
    public class UpdateProductDetailDto
    {
        public string ProductDetailId { get; set; } = default!;
        public string ProductDetailDescription { get; set; } = default!;
        public string ProductDetailInfo { get; set; } = default!;
        public string ProductId { get; set; } = default!;
    }
}
