using InstaClone.Application.DTOs.LikeDTOs;
using InstaClone.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InstaClone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikesController : ControllerBase
    {
        private readonly ILikeService _likeService;

        public LikesController(ILikeService likeService)
        {
            _likeService = likeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLikesAsync()
        {
            var likes = await _likeService.GetAllAsync();
            return Ok(likes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLikeByIdAsync(Guid id)
        {
            var like = await _likeService.GetOneAsync(id);
            return Ok(like);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLike([FromBody] LikeResponseDto like)
        {
            await _likeService.CreateAsync(like);
            return Ok(like);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLike([FromBody] LikeResponseDto like)
        {
            await _likeService.UpdateAsync(like);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLike(Guid id)
        {
            await _likeService.DeleteAsync(id);
            return NoContent();
        }

    }
}
