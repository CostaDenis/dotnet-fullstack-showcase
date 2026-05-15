using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Showcase.Api.Services.Abstractions;
using Showcase.Core.DTOs.Product;

namespace Showcase.Api.Controllers;

[ApiController]
[Authorize]
[Route("products")]
public class ProductController(IProductService ProductService)
    : ControllerBase
{

    [HttpGet]
    [ProducesResponseType(typeof(List<ProductResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<ProductResponse>>> GetAll(
        CancellationToken cancellationToken = default
    )
    {
        var products = await ProductService.GetAllAsync(cancellationToken);
        return Ok(products);
    }

    [HttpGet("categories/{categoryId:guid}")]
    [ProducesResponseType(typeof(List<ProductResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<ProductResponse>>> GetByCategoryId(
        [FromRoute] Guid categoryId,
        CancellationToken cancellationToken = default
    )
    {
        var products = await ProductService.GetByCategoryIdAsync(categoryId, cancellationToken);
        return Ok(products);
    }

    [HttpGet("{productId:guid}")]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductResponse>> GetById(
        [FromRoute] Guid productId,
        CancellationToken cancellationToken = default
    )
    {
        var product = await ProductService.GetByIdAsync(productId, cancellationToken);

        if (product is null)
            return NotFound();

        return Ok(product);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductResponse>> Create(
        [FromBody] ProductRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var product = await ProductService.CreateAsync(request, cancellationToken);

        if (product is null)
            return BadRequest();

        return CreatedAtRoute(
            new { productId = product.Id }, product
        );
    }

    [HttpPut("{productId:guid}")]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductResponse>> Update(
        [FromRoute] Guid productId,
        [FromBody] ProductRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var product = await ProductService.GetByIdAsync(productId, cancellationToken);

        if (product is null)
            return NotFound();

        var updatedProduct = await ProductService
            .UpdateAsync(productId, request, cancellationToken);

        return Ok(updatedProduct);
    }

    [HttpDelete("{productId:guid}")]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductResponse>> Delete(
        [FromRoute] Guid productId,
        CancellationToken cancellationToken = default
    )
    {
        await ProductService.DeleteAsync(productId, cancellationToken);
        return NoContent();
    }
}
