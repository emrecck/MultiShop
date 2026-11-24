using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.FeatureDtos;
using MultiShop.Catalog.Services.FeatureServices;

namespace MultiShop.Catalog.Controllers;

[Authorize]
[ApiController]
[Route("api/features")]
public sealed class FeaturesController(IFeatureService featureService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ResultFeatureDto>>> GetAll(CancellationToken cancellationToken)
        => Ok(await featureService.GetAllFeaturesAsync(cancellationToken));

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdFeatureDto>> GetById(
        [RegularExpression("^[0-9a-fA-F]{24}$")] string id,
        CancellationToken cancellationToken)
    {
        var item = await featureService.GetFeatureByIdAsync(id, cancellationToken);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateFeatureDto feature, CancellationToken cancellationToken)
    {
        var id = await featureService.CreateFeatureAsync(feature, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id }, value: null);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        [RegularExpression("^[0-9a-fA-F]{24}$")] string id,
        [FromBody] UpdateFeatureDto feature,
        CancellationToken cancellationToken)
    {
        if (!string.Equals(id, feature.FeatureId, StringComparison.OrdinalIgnoreCase))
            return BadRequest();
        var updated = await featureService.UpdateFeatureAsync(feature, cancellationToken);
        return updated ? Ok() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(
        [RegularExpression("^[0-9a-fA-F]{24}$")] string id,
        CancellationToken cancellationToken)
        => await featureService.DeleteFeatureAsync(id, cancellationToken) ? NoContent() : NotFound();
}
