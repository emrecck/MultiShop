namespace MultiShop.Catalog.Dtos.ProductDetailDtos
{
    public class GetByIdProductDetailDto
    {
        public string ProductDetailId { get; set; } = string.Empty;
        public string ProductId { get; set; } = string.Empty;
        public string ProductDetailDescription { get; set; } = string.Empty;
        public string ProductDetailInfo { get; set; } = string.Empty;
    }
}
