using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Discount.Coupons.Dtos;
using MultiShop.Discount.Coupons.Services;

namespace MultiShop.Discount.Coupons.Controllers;

[Authorize]
[ApiController]
[Route("api/coupons")]
public sealed class CouponsController(ICouponService couponService) : ControllerBase
{
    [HttpGet]
    [EndpointSummary("Get all coupons")]
    [ProducesResponseType(typeof(IReadOnlyList<ResultCouponDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ResultCouponDto>>> GetAll(CancellationToken cancellationToken)
    {
        var coupons = await couponService.GetAllCouponsAsync(cancellationToken);
        return Ok(coupons);
    }

    [HttpGet("{id:int}")]
    [EndpointSummary("Get a coupon by ID")]
    [ProducesResponseType(typeof(GetByIdCouponDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetByIdCouponDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var coupon = await couponService.GetCouponByIdAsync(id, cancellationToken);
        return coupon is null ? NotFound() : Ok(coupon);
    }

    [HttpGet("by-code/{code}")]
    [EndpointSummary("Get a coupon by code")]
    [ProducesResponseType(typeof(GetByIdCouponDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetByIdCouponDto>> GetByCode(string code, CancellationToken cancellationToken)
    {
        var coupon = await couponService.GetCouponByCodeAsync(code, cancellationToken);
        return coupon is null ? NotFound() : Ok(coupon);
    }

    [HttpGet("rate/{code}")]
    [EndpointSummary("Get discount rate by coupon code")]
    [ProducesResponseType(typeof(decimal), StatusCodes.Status200OK)]
    public async Task<ActionResult<decimal>> GetRateByCode(string code, CancellationToken cancellationToken)
    {
        var rate = await couponService.GetCouponRateByCodeAsync(code, cancellationToken);
        return Ok(rate);
    }

    [HttpGet("count")]
    [EndpointSummary("Get total coupon count")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<ActionResult<int>> GetCount(CancellationToken cancellationToken)
    {
        var count = await couponService.GetCouponCountAsync(cancellationToken);
        return Ok(count);
    }

    [HttpPost]
    [EndpointSummary("Create a coupon")]
    [ProducesResponseType(typeof(GetByIdCouponDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GetByIdCouponDto>> Create(
        [FromBody] CreateCouponDto coupon,
        CancellationToken cancellationToken)
    {
        var createdCoupon = await couponService.CreateCouponAsync(coupon, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = createdCoupon.CouponId }, createdCoupon);
    }

    [HttpPut("{id:int}")]
    [EndpointSummary("Update a coupon")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UpdateCouponDto coupon,
        CancellationToken cancellationToken)
    {
        var updated = await couponService.UpdateCouponAsync(id, coupon, cancellationToken);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    [EndpointSummary("Delete a coupon")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await couponService.DeleteCouponAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
