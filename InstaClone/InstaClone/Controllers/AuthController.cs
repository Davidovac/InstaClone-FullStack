using InstaClone.Application.DTOs.AuthDTOs;
using InstaClone.Application.Interfaces;
using InstaClone.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InstaClone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequestDto loginRequest)
        {
            LoginResponseDto loginResponseDto = await _authService.LoginAsync(loginRequest);

            return Ok(loginResponseDto);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequestDto registerRequest)
        {
            await _authService.RegisterAsync(registerRequest);

            return Ok();
        }

        [HttpGet("activate-account")]
        [AllowAnonymous]
        public async Task<IActionResult> ActivateAccountAsync([FromQuery] ActivateAccountRequestDto activateRequest)
        {
            await _authService.ActivateAccountAsync(activateRequest);

            return Ok(new { Message = "Account has been activated successfully." });
        }

        /*[HttpPost("forgot-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPasswordAsync([FromBody] ForgotPasswordRequestDto forgotRequest)
        {
            await _authService.ForgotPasswordAsync(forgotRequest);

            return Ok(new { Message = "Please check your email in order to reset your password." });
        }*/
    }
}
