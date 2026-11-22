namespace MultiShop.Cargo.Basket.Models;

public class BasketTotalDto
{
    public string UserId { get; set; } = string.Empty;
    public string? DiscountCode { get; set; }
    public decimal? DiscountRate { get; set; }
    public List<BasketItemDto> BasketItems { get; set; } = [];
    public decimal TotalPrice => BasketItems.Sum(x => x.Price * x.Quantity);
}
