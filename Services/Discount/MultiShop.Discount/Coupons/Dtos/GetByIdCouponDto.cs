namespace MultiShop.Discount.Coupons.Dtos;

/// <summary>Details of a single coupon.</summary>
public sealed record GetByIdCouponDto(
    int CouponId,
    string Code,
    decimal DiscountAmount,
    DateTimeOffset ExpirationDate,
    bool IsActive);
