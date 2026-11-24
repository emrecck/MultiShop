using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.AboutDtos;
using MultiShop.Catalog.Services.AboutServices;

namespace MultiShop.Catalog.Controllers;

[Authorize]
[ApiController]
[Route("api/abouts")]
public sealed class AboutsController(IAboutService aboutService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ResultAboutDto>>> GetAll(CancellationToken cancellationToken)
        => Ok(await aboutService.GetAllAboutsAsync(cancellationToken));

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdAboutDto>> GetById(
        [RegularExpression("^[0-9a-fA-F]{24}$")] string id,
        CancellationToken cancellationToken)
    {
        var item = await aboutService.GetAboutByIdAsync(id, cancellationToken);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAboutDto about, CancellationToken cancellationToken)
    {
        var id = await aboutService.CreateAboutAsync(about, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id }, value: null);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        [RegularExpression("^[0-9a-fA-F]{24}$")] string id,
        [FromBody] UpdateAboutDto about,
        CancellationToken cancellationToken)
    {
        if (!string.Equals(id, about.AboutId, StringComparison.OrdinalIgnoreCase))
            return BadRequest();
        var updated = await aboutService.UpdateAboutAsync(about, cancellationToken);
        return updated ? Ok() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(
        [RegularExpression("^[0-9a-fA-F]{24}$")] string id,
        CancellationToken cancellationToken)
        => await aboutService.DeleteAboutAsync(id, cancellationToken) ? NoContent() : NotFound();
}
