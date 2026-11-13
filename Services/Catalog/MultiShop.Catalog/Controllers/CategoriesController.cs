using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.CategoryDtos;
using MultiShop.Catalog.Services.CategoryServices;

namespace MultiShop.Catalog.Controllers;

[ApiController]
[Route("api/categories")]
public sealed class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    [EndpointSummary("Get all categories")]
    [ProducesResponseType(typeof(IEnumerable<ResultCategoryDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ResultCategoryDto>>> GetAll(CancellationToken cancellationToken)
    {
        var categories = await _categoryService.GetAllCategoriesAsync(cancellationToken);
        return Ok(categories);
    }

    [HttpGet("{id}")]
    [EndpointSummary("Get a category by ID")]
    [ProducesResponseType(typeof(GetByIdCategoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetByIdCategoryDto>> GetById(
        [RegularExpression("^[0-9a-fA-F]{24}$")] string id,
        CancellationToken cancellationToken)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id, cancellationToken);
        return category is null ? NotFound() : Ok(category);
    }

    [HttpPost]
    [EndpointSummary("Create a category")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(
        [FromBody] CreateCategoryDto category,
        CancellationToken cancellationToken)
    {
        var id = await _categoryService.CreateCategoryAsync(category, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id }, value: null);
    }

    [HttpPut("{id}")]
    [EndpointSummary("Update a category")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        [RegularExpression("^[0-9a-fA-F]{24}$")] string id,
        [FromBody] UpdateCategoryDto category,
        CancellationToken cancellationToken)
    {
        if (!string.Equals(id, category.Id, StringComparison.OrdinalIgnoreCase))
        {
            ModelState.AddModelError(nameof(category.Id), "The route ID must match the request body ID.");
            return ValidationProblem(ModelState);
        }

        var updated = await _categoryService.UpdateCategoryAsync(category, cancellationToken);
        return updated ? Ok() : NotFound();
    }

    [HttpDelete("{id}")]
    [EndpointSummary("Delete a category")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        [RegularExpression("^[0-9a-fA-F]{24}$")] string id,
        CancellationToken cancellationToken)
    {
        var deleted = await _categoryService.DeleteCategoryAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
