using InstaClone.Application.DTOs.CommentDTOs;
using InstaClone.Application.DTOs.LikeDTOs;
using InstaClone.Application.DTOs.PostDTOs;
using InstaClone.Application.Interfaces;
using InstaClone.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InstaClone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;

        public PostsController(IPostService postService, ICommentService commentService)
        {
            _postService = postService;
            _commentService = commentService;
        }

        [HttpGet("feed")]
        public async Task<IActionResult> GetPostsFeedAsync()
        {
            var posts = await _postService.GetUserFeedAsync(User);
            return Ok(posts);
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetPostsByUserAsync()
        {
            var posts = await _postService.GetByUserAsync(User);
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostByIdAsync(Guid id)
        {
            var post = await _postService.GetOneAsync(id);
            return Ok(post);
        }

        [HttpGet("{id}/comments")]
        public async Task<IActionResult> GetPostCommentsAsync(Guid id)
        {
            var comments = await _commentService.GetPostCommentsAsync(id);
            return Ok(comments);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePostAsync([FromForm] PostCreateRequestDto post, IFormFile? photo)
        {
            await _postService.CreateAsync(post, photo, User);
            return Ok(post);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePostAsync([FromBody] PostUpdateRequestDto post)
        {
            await _postService.UpdateAsync(post, User);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePostAsync(Guid id)
        {
            await _postService.DeleteAsync(id, User);
            return NoContent();
        }

        [HttpPost("{postId}/like")]
        public async Task<IActionResult> LikePostAsync(Guid postId)
        {
            await _postService.LikePostAsync(postId, User);
            return Ok();
        }

        [HttpDelete("{postId}/like")]
        public async Task<IActionResult> UnlikePostAsync(Guid postId)
        {
            await _postService.UnlikePostAsync(postId, User);
            return NoContent();
        }

        [HttpPost("{postId}/comment")]
        public async Task<IActionResult> CommentOnPostAsync(Guid postId, [FromBody] CommentCreateRequestDto comment)
        {
            var newComment = await _postService.CommentOnPostAsync(postId, comment, User);
            return Ok(newComment);
        }

        [HttpPost("{postId}/comment/{commentId}/reply")]
        public async Task<IActionResult> ReplyOnCommentOnThisPostAsync(Guid postId, Guid commentId, [FromBody] ReplyCreateRequestDto reply)
        {
            var newReply = await _postService.ReplyOnCommentOnThisPostAsync(postId, commentId, reply, User);
            return Ok(newReply);
        }
    }
}
