using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Comments.Dtos;
using MultiShop.Comments.Services;

namespace MultiShop.Comments.Controllers;

[Authorize]
[ApiController]
[Route("api/comments")]
public sealed class CommentsController(ICommentService commentService) : ControllerBase
{
    [HttpGet]
    [EndpointSummary("Get all comments")]
    [ProducesResponseType(typeof(List<ResultCommentDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<ResultCommentDto>>> GetAll(CancellationToken cancellationToken)
    {
        var values = await commentService.GetAllCommentAsync(cancellationToken);
        return Ok(values);
    }

    [HttpGet("{id:int}")]
    [EndpointSummary("Get a comment by ID")]
    [ProducesResponseType(typeof(GetByIdCommentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetByIdCommentDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var value = await commentService.GetByIdCommentAsync(id, cancellationToken);
        return value is null ? NotFound() : Ok(value);
    }

    [HttpGet("CommentListByProductId/{id}")]
    [EndpointSummary("Get comments by product ID")]
    [ProducesResponseType(typeof(List<ResultCommentDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<ResultCommentDto>>> CommentListByProductId(string id, CancellationToken cancellationToken)
    {
        var values = await commentService.CommentListByProductIdAsync(id, cancellationToken);
        return Ok(values);
    }

    [HttpPost]
    [EndpointSummary("Create a comment")]
    [ProducesResponseType(typeof(GetByIdCommentDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GetByIdCommentDto>> Create(
        [FromBody] CreateCommentDto dto,
        CancellationToken cancellationToken)
    {
        var created = await commentService.CreateCommentAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.UserCommentId }, created);
    }

    [HttpPut("{id:int}")]
    [EndpointSummary("Update a comment")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UpdateCommentDto dto,
        CancellationToken cancellationToken)
    {
        if (dto.UserCommentId != 0 && dto.UserCommentId != id)
        {
            return BadRequest("Route ID must match body UserCommentId.");
        }

        dto.UserCommentId = id;
        var updated = await commentService.UpdateCommentAsync(dto, cancellationToken);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    [EndpointSummary("Delete a comment")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await commentService.DeleteCommentAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }

    [HttpGet("GetTotalCommentCount")]
    [EndpointSummary("Get total comment count")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<ActionResult<int>> GetTotalCommentCount(CancellationToken cancellationToken)
    {
        var count = await commentService.GetTotalCommentCountAsync(cancellationToken);
        return Ok(count);
    }

    [HttpGet("GetActiveCommentCount")]
    [EndpointSummary("Get active comment count")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<ActionResult<int>> GetActiveCommentCount(CancellationToken cancellationToken)
    {
        var count = await commentService.GetActiveCommentCountAsync(cancellationToken);
        return Ok(count);
    }

    [HttpGet("GetPassiveCommentCount")]
    [EndpointSummary("Get passive comment count")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<ActionResult<int>> GetPassiveCommentCount(CancellationToken cancellationToken)
    {
        var count = await commentService.GetPassiveCommentCountAsync(cancellationToken);
        return Ok(count);
    }
}
