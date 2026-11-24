using MultiShop.Discount.Coupons.Dtos;

namespace MultiShop.Discount.Coupons.Services;

public interface ICouponService
{
    Task<IReadOnlyList<ResultCouponDto>> GetAllCouponsAsync(CancellationToken cancellationToken);
    Task<GetByIdCouponDto?> GetCouponByIdAsync(int id, CancellationToken cancellationToken);
    Task<GetByIdCouponDto?> GetCouponByCodeAsync(string code, CancellationToken cancellationToken);
    Task<decimal> GetCouponRateByCodeAsync(string code, CancellationToken cancellationToken);
    Task<int> GetCouponCountAsync(CancellationToken cancellationToken);
    Task<GetByIdCouponDto> CreateCouponAsync(CreateCouponDto coupon, CancellationToken cancellationToken);
    Task<bool> UpdateCouponAsync(int id, UpdateCouponDto coupon, CancellationToken cancellationToken);
    Task<bool> DeleteCouponAsync(int id, CancellationToken cancellationToken);
}
