using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.ProductDtos;
using MultiShop.Catalog.Services.ProductServices;

namespace MultiShop.Catalog.Controllers;

[ApiController]
[Route("api/products")]
public sealed class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    [EndpointSummary("Get all products")]
    [ProducesResponseType(typeof(IEnumerable<ResultProductDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ResultProductDto>>> GetAll(CancellationToken cancellationToken)
    {
        var products = await _productService.GetAllProductsAsync(cancellationToken);
        return Ok(products);
    }

    [HttpGet("{id}")]
    [EndpointSummary("Get a product by ID")]
    [ProducesResponseType(typeof(GetByIdProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetByIdProductDto>> GetById(
        [RegularExpression("^[0-9a-fA-F]{24}$")] string id,
        CancellationToken cancellationToken)
    {
        var product = await _productService.GetProductByIdAsync(id, cancellationToken);
        return product is null ? NotFound() : Ok(product);
    }

    [HttpPost]
    [EndpointSummary("Create a product")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(
        [FromBody] CreateProductDto product,
        CancellationToken cancellationToken)
    {
        var id = await _productService.CreateProductAsync(product, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id }, value: null);
    }

    [HttpPut("{id}")]
    [EndpointSummary("Update a product")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        [RegularExpression("^[0-9a-fA-F]{24}$")] string id,
        [FromBody] UpdateProductDto product,
        CancellationToken cancellationToken)
    {
        if (!string.Equals(id, product.ProductId, StringComparison.OrdinalIgnoreCase))
        {
            ModelState.AddModelError(nameof(product.ProductId), "The route ID must match the request body ID.");
            return ValidationProblem(ModelState);
        }

        var updated = await _productService.UpdateProductAsync(product, cancellationToken);
        return updated ? Ok() : NotFound();
    }

    [HttpDelete("{id}")]
    [EndpointSummary("Delete a product")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        [RegularExpression("^[0-9a-fA-F]{24}$")] string id,
        CancellationToken cancellationToken)
    {
        var deleted = await _productService.DeleteProductAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
