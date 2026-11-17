using System.ComponentModel.DataAnnotations;

namespace MultiShop.Discount.Coupons.Dtos;

/// <summary>Data used to update an existing coupon.</summary>
public sealed record UpdateCouponDto
{
    [Required]
    public required string Code { get; init; }

    [Range(typeof(decimal), "0.01", "100.00", ParseLimitsInInvariantCulture = true)]
    public required decimal DiscountAmount { get; init; }

    public required DateTimeOffset ExpirationDate { get; init; }

    public required bool IsActive { get; init; }
}
