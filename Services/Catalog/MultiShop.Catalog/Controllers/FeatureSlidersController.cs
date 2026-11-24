using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.FeatureSliderDtos;
using MultiShop.Catalog.Services.FeatureSliderServices;

namespace MultiShop.Catalog.Controllers;

[Authorize]
[ApiController]
[Route("api/feature-sliders")]
public sealed class FeatureSlidersController(IFeatureSliderService featureSliderService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ResultFeatureSliderDto>>> GetAll(CancellationToken cancellationToken)
        => Ok(await featureSliderService.GetAllFeatureSlidersAsync(cancellationToken));

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdFeatureSliderDto>> GetById(
        [RegularExpression("^[0-9a-fA-F]{24}$")] string id,
        CancellationToken cancellationToken)
    {
        var item = await featureSliderService.GetFeatureSliderByIdAsync(id, cancellationToken);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateFeatureSliderDto featureSlider, CancellationToken cancellationToken)
    {
        var id = await featureSliderService.CreateFeatureSliderAsync(featureSlider, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id }, value: null);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        [RegularExpression("^[0-9a-fA-F]{24}$")] string id,
        [FromBody] UpdateFeatureSliderDto featureSlider,
        CancellationToken cancellationToken)
    {
        if (!string.Equals(id, featureSlider.FeatureSliderId, StringComparison.OrdinalIgnoreCase))
            return BadRequest();
        var updated = await featureSliderService.UpdateFeatureSliderAsync(featureSlider, cancellationToken);
        return updated ? Ok() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(
        [RegularExpression("^[0-9a-fA-F]{24}$")] string id,
        CancellationToken cancellationToken)
        => await featureSliderService.DeleteFeatureSliderAsync(id, cancellationToken) ? NoContent() : NotFound();

    [HttpGet("FeatureSliderChangeStatusToTrue/{id}")]
    public async Task<IActionResult> ChangeStatusToTrue(
        [RegularExpression("^[0-9a-fA-F]{24}$")] string id,
        CancellationToken cancellationToken)
        => await featureSliderService.ChangeStatusAsync(id, true, cancellationToken) ? Ok() : NotFound();

    [HttpGet("FeatureSliderChangeStatusToFalse/{id}")]
    public async Task<IActionResult> ChangeStatusToFalse(
        [RegularExpression("^[0-9a-fA-F]{24}$")] string id,
        CancellationToken cancellationToken)
        => await featureSliderService.ChangeStatusAsync(id, false, cancellationToken) ? Ok() : NotFound();
}
