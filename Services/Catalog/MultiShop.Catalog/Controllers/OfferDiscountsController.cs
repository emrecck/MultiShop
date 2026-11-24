using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.OfferDiscountDtos;
using MultiShop.Catalog.Services.OfferDiscountServices;

namespace MultiShop.Catalog.Controllers;

[Authorize]
[ApiController]
[Route("api/offer-discounts")]
public sealed class OfferDiscountsController(IOfferDiscountService offerDiscountService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ResultOfferDiscountDto>>> GetAll(CancellationToken cancellationToken)
        => Ok(await offerDiscountService.GetAllOfferDiscountsAsync(cancellationToken));

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdOfferDiscountDto>> GetById(
        [RegularExpression("^[0-9a-fA-F]{24}$")] string id,
        CancellationToken cancellationToken)
    {
        var item = await offerDiscountService.GetOfferDiscountByIdAsync(id, cancellationToken);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOfferDiscountDto offerDiscount, CancellationToken cancellationToken)
    {
        var id = await offerDiscountService.CreateOfferDiscountAsync(offerDiscount, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id }, value: null);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        [RegularExpression("^[0-9a-fA-F]{24}$")] string id,
        [FromBody] UpdateOfferDiscountDto offerDiscount,
        CancellationToken cancellationToken)
    {
        if (!string.Equals(id, offerDiscount.OfferDiscountId, StringComparison.OrdinalIgnoreCase))
            return BadRequest();
        var updated = await offerDiscountService.UpdateOfferDiscountAsync(offerDiscount, cancellationToken);
        return updated ? Ok() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(
        [RegularExpression("^[0-9a-fA-F]{24}$")] string id,
        CancellationToken cancellationToken)
        => await offerDiscountService.DeleteOfferDiscountAsync(id, cancellationToken) ? NoContent() : NotFound();
}
