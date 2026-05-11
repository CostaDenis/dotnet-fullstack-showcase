using Microsoft.AspNetCore.Mvc;
using Showcase.Api.Services.Abstractions;
using Showcase.Core.DTOs.Category;

namespace Showcase.Api.Controllers;

[ApiController]
[Route("categories")]
public class CategoryController(ICategoryService CategoryService)
    : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(List<CategoryResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<CategoryResponse>>> GetAll(
        CancellationToken cancellationToken = default
    )
    {
        var categories = await CategoryService.GetAllAsync(cancellationToken);

        return Ok(categories);
    }

    [HttpGet("{categoryId:guid}")]
    [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoryResponse>> GetById(
        [FromRoute] Guid categoryId,
        CancellationToken cancellationToken = default
    )
    {
        var category = await CategoryService.GetByIdAsync(categoryId, cancellationToken);

        if (category is null)
            return NotFound();

        return Ok(category);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CategoryResponse>> Create(
        [FromBody] CategoryRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var newCategory = await CategoryService.CreateAsync(request, cancellationToken);

        if (newCategory is null)
            return BadRequest();

        return CreatedAtRoute(
           new { categoryId = newCategory.Id }, newCategory
        );
    }

    [HttpPut("{categoryId:guid}")]
    [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoryResponse>> Update(
        [FromRoute] Guid categoryId,
        [FromBody] CategoryRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var category = await CategoryService.GetByIdAsync(categoryId, cancellationToken);

        if (category is null)
            return NotFound();

        var updatedCategory = await CategoryService.UpdateAsync(categoryId, request, cancellationToken);

        return Ok(updatedCategory);
    }

    [HttpDelete("{categoryId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Delete(
        [FromRoute] Guid categoryId,
        CancellationToken cancellationToken = default
    )
    {
        await CategoryService.DeleteAsync(categoryId, cancellationToken);

        return NoContent();
    }
}
