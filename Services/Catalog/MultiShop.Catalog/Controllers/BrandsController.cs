using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.BrandDtos;
using MultiShop.Catalog.Services.BrandServices;

namespace MultiShop.Catalog.Controllers;

[Authorize]
[ApiController]
[Route("api/brands")]
public sealed class BrandsController(IBrandService brandService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ResultBrandDto>>> GetAll(CancellationToken cancellationToken)
        => Ok(await brandService.GetAllBrandsAsync(cancellationToken));

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdBrandDto>> GetById(
        [RegularExpression("^[0-9a-fA-F]{24}$")] string id,
        CancellationToken cancellationToken)
    {
        var item = await brandService.GetBrandByIdAsync(id, cancellationToken);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBrandDto brand, CancellationToken cancellationToken)
    {
        var id = await brandService.CreateBrandAsync(brand, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id }, value: null);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        [RegularExpression("^[0-9a-fA-F]{24}$")] string id,
        [FromBody] UpdateBrandDto brand,
        CancellationToken cancellationToken)
    {
        if (!string.Equals(id, brand.BrandId, StringComparison.OrdinalIgnoreCase))
            return BadRequest();
        var updated = await brandService.UpdateBrandAsync(brand, cancellationToken);
        return updated ? Ok() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(
        [RegularExpression("^[0-9a-fA-F]{24}$")] string id,
        CancellationToken cancellationToken)
        => await brandService.DeleteBrandAsync(id, cancellationToken) ? NoContent() : NotFound();
}
