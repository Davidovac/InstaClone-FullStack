using InstaClone.Application.DTOs.UserDTOs;
using InstaClone.Application.Interfaces;
using InstaClone.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InstaClone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var users = await _userService.GetAllAsync();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);

            return Ok(user);
        }

        [HttpGet("{id}/following")]
        public async Task<IActionResult> GetFollowingByUserAsync(Guid id)
        {
            var following = await _userService.GetFollowingByUser(id);

            return Ok(following);
        }

        [HttpGet("{id}/followers")]
        public async Task<IActionResult> GetFollowersByUserAsync(Guid id)
        {
            var followers = await _userService.GetFollowersByUser(id);

            return Ok(followers);
        }

        [HttpGet("{userName}/profile")]
        public async Task<IActionResult> GetProfileAsync(string userName)
        {
            var user = await _userService.GetProfileAsync(userName);

            return Ok(user);
        }

        [HttpPost("{userName}/follow-profile")]
        public async Task<IActionResult> FollowProfileAsync(string userName)
        {
            await _userService.FollowProfileAsync(userName, User);

            return Ok();
        }

        [HttpDelete("{userName}/unfollow-profile")]
        public async Task<IActionResult> UnfollowProfileAsync(string userName)
        {
            await _userService.UnfollowProfileAsync(userName, User);

            return Ok();
        }

        [HttpGet("{userName}/followed-profile-check")]
        public async Task<IActionResult> FollowedProfileCheckAsync(string userName)
        {
            var check = await _userService.FollowedProfileCheckAsync(userName, User);

            return Ok(check);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UserUpdateRequestDto userData)
        {
            var user = await _userService.UpdateAsync(id, userData);
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _userService.DeleteAsync(id);
            return NoContent();
        }
    }
}
