using System.ComponentModel.DataAnnotations;

namespace MultiShop.Discount.Coupons.Dtos;

/// <summary>Data required to create a coupon.</summary>
public sealed record CreateCouponDto
{
    [Required]
    public required string Code { get; init; }

    [Range(typeof(decimal), "0.01", "100.00", ParseLimitsInInvariantCulture = true)]
    public required decimal DiscountAmount { get; init; }

    public required DateTimeOffset ExpirationDate { get; init; }

    public required bool IsActive { get; init; }
}
