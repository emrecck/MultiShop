using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.ProductImageDtos;
using MultiShop.Catalog.Services.ProductImageServices;

namespace MultiShop.Catalog.Controllers;

[ApiController]
[Route("api/product-images")]
public sealed class ProductImagesController : ControllerBase
{
    private readonly IProductImageService _productImageService;

    public ProductImagesController(IProductImageService productImageService)
    {
        _productImageService = productImageService;
    }

    [HttpGet]
    [EndpointSummary("Get all product images")]
    [ProducesResponseType(typeof(IEnumerable<ResultProductImageDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ResultProductImageDto>>> GetAll(CancellationToken cancellationToken)
    {
        var productImages = await _productImageService.GetAllProductImagesAsync(cancellationToken);
        return Ok(productImages);
    }

    [HttpGet("{id}")]
    [EndpointSummary("Get a product image by ID")]
    [ProducesResponseType(typeof(GetByIdProductImageDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetByIdProductImageDto>> GetById(
        [RegularExpression("^[0-9a-fA-F]{24}$")] string id,
        CancellationToken cancellationToken)
    {
        var productImage = await _productImageService.GetProductImageByIdAsync(id, cancellationToken);
        return productImage is null ? NotFound() : Ok(productImage);
    }

    [HttpPost]
    [EndpointSummary("Create a product image")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(
        [FromBody] CreateProductImageDto productImage,
        CancellationToken cancellationToken)
    {
        var id = await _productImageService.CreateProductImageAsync(productImage, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id }, value: null);
    }

    [HttpPut("{id}")]
    [EndpointSummary("Update a product image")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        [RegularExpression("^[0-9a-fA-F]{24}$")] string id,
        [FromBody] UpdateProductImageDto productImage,
        CancellationToken cancellationToken)
    {
        if (!string.Equals(id, productImage.Id, StringComparison.OrdinalIgnoreCase))
        {
            ModelState.AddModelError(nameof(productImage.Id), "The route ID must match the request body ID.");
            return ValidationProblem(ModelState);
        }

        var updated = await _productImageService.UpdateProductImageAsync(productImage, cancellationToken);
        return updated ? Ok() : NotFound();
    }

    [HttpDelete("{id}")]
    [EndpointSummary("Delete a product image")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        [RegularExpression("^[0-9a-fA-F]{24}$")] string id,
        CancellationToken cancellationToken)
    {
        var deleted = await _productImageService.DeleteProductImageAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
