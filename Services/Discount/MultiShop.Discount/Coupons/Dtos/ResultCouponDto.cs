namespace MultiShop.Discount.Coupons.Dtos;

/// <summary>Coupon data returned in a collection.</summary>
public sealed record ResultCouponDto(
    int CouponId,
    string Code,
    decimal DiscountAmount,
    DateTimeOffset ExpirationDate,
    bool IsActive);
