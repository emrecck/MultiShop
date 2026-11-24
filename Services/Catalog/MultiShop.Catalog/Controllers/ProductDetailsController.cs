using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MultiShop.Catalog.Dtos.ProductDetailDtos;
using MultiShop.Catalog.Services.ProductDetailServices;

namespace MultiShop.Catalog.Controllers;

[Authorize]
[ApiController]
[Route("api/product-details")]
public sealed class ProductDetailsController : ControllerBase
{
    private readonly IProductDetailService _productDetailService;

    public ProductDetailsController(IProductDetailService productDetailService)
    {
        _productDetailService = productDetailService;
    }

    [HttpGet]
    [EndpointSummary("Get all product details")]
    [ProducesResponseType(typeof(IEnumerable<ResultProductDetailDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ResultProductDetailDto>>> GetAll(CancellationToken cancellationToken)
    {
        var productDetails = await _productDetailService.GetAllProductDetailsAsync(cancellationToken);
        return Ok(productDetails);
    }

    [HttpGet("{id}")]
    [EndpointSummary("Get product details by ID")]
    [ProducesResponseType(typeof(GetByIdProductDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetByIdProductDetailDto>> GetById(
        [RegularExpression("^[0-9a-fA-F]{24}$")] string id,
        CancellationToken cancellationToken)
    {
        var productDetail = await _productDetailService.GetProductDetailByIdAsync(id, cancellationToken);
        return productDetail is null ? NotFound() : Ok(productDetail);
    }

    [HttpGet("by-product/{productId}")]
    [EndpointSummary("Get product details by product ID")]
    [ProducesResponseType(typeof(GetByIdProductDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetByIdProductDetailDto>> GetByProductId(
        string productId,
        CancellationToken cancellationToken)
    {
        var productDetail = await _productDetailService.GetProductDetailByProductIdAsync(productId, cancellationToken);
        return productDetail is null ? NotFound() : Ok(productDetail);
    }

    [HttpPost]
    [EndpointSummary("Create product details")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(
        [FromBody] CreateProductDetailDto productDetail,
        CancellationToken cancellationToken)
    {
        var id = await _productDetailService.CreateProductDetailAsync(productDetail, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id }, value: null);
    }

    [HttpPut("{id}")]
    [EndpointSummary("Update product details")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        [RegularExpression("^[0-9a-fA-F]{24}$")] string id,
        [FromBody] UpdateProductDetailDto productDetail,
        CancellationToken cancellationToken)
    {
        var updated = await _productDetailService.UpdateProductDetailAsync(productDetail, cancellationToken);
        return updated ? Ok() : NotFound();
    }

    [HttpDelete("{id}")]
    [EndpointSummary("Delete product details")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        [RegularExpression("^[0-9a-fA-F]{24}$")] string id,
        CancellationToken cancellationToken)
    {
        var deleted = await _productDetailService.DeleteProductDetailAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
