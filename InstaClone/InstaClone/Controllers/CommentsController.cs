using InstaClone.Application.DTOs.CommentDTOs;
using InstaClone.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InstaClone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCommentsAsync()
        {
            var comments = await _commentService.GetAllAsync();
            return Ok(comments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentByIdAsync(Guid id)
        {
            var comment = await _commentService.GetOneAsync(id);
            return Ok(comment);
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CommentCreateRequestDto comment)
        {
            await _commentService.CreateAsync(comment, User);
            return Ok(comment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(Guid id, [FromBody] CommentUpdateRequestDto comment)
        {
            await _commentService.UpdateAsync(id, comment, User);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            await _commentService.DeleteAsync(id, User);
            return NoContent();
        }

    }
}
