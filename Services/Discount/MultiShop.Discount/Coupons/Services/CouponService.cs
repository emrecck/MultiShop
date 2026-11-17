using Dapper;
using MultiShop.Discount.Coupons.Dtos;

namespace MultiShop.Discount.Coupons.Services;

public sealed class CouponService(DapperContext context) : ICouponService
{
    private const string CouponSelect = """
        SELECT
            CouponId,
            Code,
            DiscountAmount,
            TODATETIMEOFFSET(ExpirationDate, '+00:00') AS ExpirationDate,
            IsActive
        FROM Coupons
        """;

    public async Task<IReadOnlyList<ResultCouponDto>> GetAllCouponsAsync(CancellationToken cancellationToken)
    {
        using var connection = context.CreateConnection();
        var coupons = await connection.QueryAsync<ResultCouponDto>(
            new CommandDefinition(CouponSelect, cancellationToken: cancellationToken));

        return coupons.AsList();
    }

    public async Task<GetByIdCouponDto?> GetCouponByIdAsync(int id, CancellationToken cancellationToken)
    {
        using var connection = context.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<GetByIdCouponDto>(
            new CommandDefinition(
                $"{CouponSelect} WHERE CouponId = @Id",
                new { Id = id },
                cancellationToken: cancellationToken));
    }

    public async Task<GetByIdCouponDto> CreateCouponAsync(
        CreateCouponDto coupon,
        CancellationToken cancellationToken)
    {
        const string sql = """
            INSERT INTO Coupons (Code, DiscountAmount, ExpirationDate, IsActive)
            VALUES (@Code, @DiscountAmount, @ExpirationDate, @IsActive);
            SELECT CAST(SCOPE_IDENTITY() AS int);
            """;

        using var connection = context.CreateConnection();
        var id = await connection.ExecuteScalarAsync<int>(
            new CommandDefinition(
                sql,
                new
                {
                    coupon.Code,
                    coupon.DiscountAmount,
                    ExpirationDate = coupon.ExpirationDate.UtcDateTime,
                    coupon.IsActive
                },
                cancellationToken: cancellationToken));

        return new GetByIdCouponDto(
            id,
            coupon.Code,
            coupon.DiscountAmount,
            coupon.ExpirationDate.ToUniversalTime(),
            coupon.IsActive);
    }

    public async Task<bool> UpdateCouponAsync(
        int id,
        UpdateCouponDto coupon,
        CancellationToken cancellationToken)
    {
        const string sql = """
            UPDATE Coupons
            SET Code = @Code,
                DiscountAmount = @DiscountAmount,
                ExpirationDate = @ExpirationDate,
                IsActive = @IsActive
            WHERE CouponId = @Id
            """;

        using var connection = context.CreateConnection();
        var affectedRows = await connection.ExecuteAsync(
            new CommandDefinition(
                sql,
                new
                {
                    Id = id,
                    coupon.Code,
                    coupon.DiscountAmount,
                    ExpirationDate = coupon.ExpirationDate.UtcDateTime,
                    coupon.IsActive
                },
                cancellationToken: cancellationToken));

        return affectedRows > 0;
    }

    public async Task<bool> DeleteCouponAsync(int id, CancellationToken cancellationToken)
    {
        using var connection = context.CreateConnection();
        var affectedRows = await connection.ExecuteAsync(
            new CommandDefinition(
                "DELETE FROM Coupons WHERE CouponId = @Id",
                new { Id = id },
                cancellationToken: cancellationToken));

        return affectedRows > 0;
    }
}
