using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.SpecialOfferDtos;
using MultiShop.Catalog.Services.SpecialOfferServices;

namespace MultiShop.Catalog.Controllers;

[Authorize]
[ApiController]
[Route("api/special-offers")]
public sealed class SpecialOffersController(ISpecialOfferService specialOfferService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ResultSpecialOfferDto>>> GetAll(CancellationToken cancellationToken)
        => Ok(await specialOfferService.GetAllSpecialOffersAsync(cancellationToken));

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdSpecialOfferDto>> GetById(
        [RegularExpression("^[0-9a-fA-F]{24}$")] string id,
        CancellationToken cancellationToken)
    {
        var item = await specialOfferService.GetSpecialOfferByIdAsync(id, cancellationToken);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSpecialOfferDto specialOffer, CancellationToken cancellationToken)
    {
        var id = await specialOfferService.CreateSpecialOfferAsync(specialOffer, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id }, value: null);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        [RegularExpression("^[0-9a-fA-F]{24}$")] string id,
        [FromBody] UpdateSpecialOfferDto specialOffer,
        CancellationToken cancellationToken)
    {
        if (!string.Equals(id, specialOffer.SpecialOfferId, StringComparison.OrdinalIgnoreCase))
            return BadRequest();
        var updated = await specialOfferService.UpdateSpecialOfferAsync(specialOffer, cancellationToken);
        return updated ? Ok() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(
        [RegularExpression("^[0-9a-fA-F]{24}$")] string id,
        CancellationToken cancellationToken)
        => await specialOfferService.DeleteSpecialOfferAsync(id, cancellationToken) ? NoContent() : NotFound();
}
