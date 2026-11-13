namespace MultiShop.Catalog.Dtos.ProductDtos
{
	public class GetByIdProductDto
	{
		public string ProductId { get; set; } = string.Empty;
		public string ProductName { get; set; } = string.Empty;
		public string ProductDescription { get; set; } = string.Empty;
		public decimal ProductPrice { get; set; }
		public string ProductImageUrl { get; set; } = string.Empty;
		public int ProductStock { get; set; }
		public string CategoryId { get; set; } = string.Empty;
	}
}
