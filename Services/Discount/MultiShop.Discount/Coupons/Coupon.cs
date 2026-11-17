namespace MultiShop.Discount.Coupons;

public class Coupon
{
    public int CouponId { get; set; }
    public string Code { get; set; } = default!;
    public decimal DiscountAmount { get; set; }
    public DateTime ExpirationDate { get; set; }
    public bool IsActive { get; set; }
}