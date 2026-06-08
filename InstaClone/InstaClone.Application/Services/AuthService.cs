using AutoMapper;
using InstaClone.Application.DTOs.AuthDTOs;
using InstaClone.Application.Exceptions;
using InstaClone.Application.Interfaces;
using InstaClone.Application.Settings;
using InstaClone.Domain.Entities;
using InstaClone.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using System.Text;

namespace InstaClone.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOptions<FrontendOptions> _frontendOptions;
        private readonly IEmailSender _emailSender;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, ITokenService tokenService, IUnitOfWork unitOfWork, IOptions<FrontendOptions> frontendOptions, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
            _frontendOptions = frontendOptions;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequest)
        {
            var user = await _userManager.FindByNameAsync(loginRequest.UserName);
            if (user == null)
            {
                throw new BadRequestException("Invalid username or password.");
            }

            var emailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
            if (emailConfirmed == false)
            {
                throw new BadRequestException("Please confirm your email address before logging in.");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginRequest.Password, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                throw new BadRequestException("Invalid username or password.");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = _tokenService.CreateToken(user, roles.ToList());

            var loginResponse = new LoginResponseDto
            {
                Token = token
            };

            return loginResponse;
        }

        public async Task RegisterAsync(RegisterRequestDto registerRequest)
        {
            bool isPasswordValid = registerRequest.ValidatePassword();
            if (!isPasswordValid)
                throw new BadRequestException("Password does not meet the required criteria.");

            User user = _mapper.Map<User>(registerRequest);

            user.EmailConfirmed = false;

            bool emailExists = await _userManager.FindByEmailAsync(registerRequest.Email) != null;
            if (emailExists)
                throw new BadRequestException("An account with this email already exists.");

            bool usernameExists = await _userManager.FindByNameAsync(registerRequest.UserName) != null;
            if (usernameExists)
                throw new BadRequestException("A user with this username already exists.");

            var createResult = await _userManager.CreateAsync(user, registerRequest.Password);
            if (!createResult.Succeeded)
            {
                throw new BadRequestException("Something went wrong while creating the account.");
            }

            await SendActivationEmailAsync(user);
        }

        private async Task SendActivationEmailAsync(User user)
        {
            try
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var tokenBytes = Encoding.UTF8.GetBytes(token);
                var encodedToken = WebEncoders.Base64UrlEncode(tokenBytes);
                var frontendUrl = _frontendOptions.Value.FrontendBaseUrl;

                if (string.IsNullOrEmpty(frontendUrl))
                {
                    throw new InvalidOperationException("FrontendBaseUrl isn't configured in appsettings.");
                }

                var activationLink = $"{frontendUrl}/activate-account?email={user.Email}&token={encodedToken}";
                var emailSubject = "Aktivirajte Vaš Nalog - InstaClone App";
                var htmlMessage = $@"
                <h1>Dobrodošli u InstaClone App!</h1>
                <p>Molimo Vas da aktivirate Vaš nalog klikom na link ispod:</p>
                <a href='{activationLink}'>Aktiviraj Nalog</a>";

                await _emailSender.SendEmailAsync(user.Email!, emailSubject, htmlMessage);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Issue upon sending an activation email: {ex.Message}");
            }
        }

        public async Task ActivateAccountAsync(ActivateAccountRequestDto activateRequest)
        {
            if (string.IsNullOrEmpty(activateRequest.Email) || string.IsNullOrEmpty(activateRequest.Token))
            {
                throw new BadRequestException("Email and token are required.");
            }

            var user = await _userManager.FindByEmailAsync(activateRequest.Email);
            if (user == null)
            {
                throw new NotFoundException("User");
            }

            var tokenBytes = WebEncoders.Base64UrlDecode(activateRequest.Token);
            var decodedToken = Encoding.UTF8.GetString(tokenBytes);

            var result = await _userManager.ConfirmEmailAsync(user, decodedToken);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new BadRequestException($"Activation failed: {errors}");
            }
        }

        public async Task ForgotPasswordAsync(ForgotPasswordRequestDto forgotRequest)
        {
            if (string.IsNullOrEmpty(forgotRequest.Email))
            {
                throw new BadRequestException("Email is required.");
            }

            var user = await _userManager.FindByEmailAsync(forgotRequest.Email);

            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                throw new NotFoundException("User");
            }

            try
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                var tokenBytes = Encoding.UTF8.GetBytes(token);
                var encodedToken = WebEncoders.Base64UrlEncode(tokenBytes);

                var frontendUrl = _frontendOptions.Value.FrontendBaseUrl;
                if (string.IsNullOrEmpty(frontendUrl))
                {
                    throw new InvalidOperationException("FrontendBaseUrl isn't configured in appsettings.");
                }

                var resetLink = $"{frontendUrl}/reset-password?email={user.Email}&token={encodedToken}";

                var emailSubject = "Resetovanje Lozinke - Delivery App";
                var htmlMessage = $@"
                <h1>Zahtev za resetovanje lozinke</h1>
                <p>Dobili smo zahtev za resetovanje Vaše lozinke. Ako niste Vi podneli ovaj zahtev, ignorišite ovaj email.</p>
                <p>Za promenu lozinke, kliknite na link ispod:</p>
                <a href='{resetLink}' style='padding: 10px 15px; background-color: #dc3545; color: white; text-decoration: none; border-radius: 5px;'>
                    Resetuj Lozinku
                </a>
                <br>
                <p>Ako link ne radi, iskopirajte ovu adresu u Vaš pretraživač:</p>
                <p>{resetLink}</p>";

                await _emailSender.SendEmailAsync(user.Email, emailSubject, htmlMessage);
            }
            catch (Exception ex)
            {
                return;
            }
        }

        public async Task ResetPasswordAsync(ResetPasswordRequestDto resetRequest)
        {
            if (resetRequest == null || string.IsNullOrEmpty(resetRequest.Email) || string.IsNullOrEmpty(resetRequest.Token))
            {
                throw new BadRequestException("Invalid request.");
            }
            var user = await _userManager.FindByEmailAsync(resetRequest.Email);

            if (user == null)
            {
                throw new BadRequestException("Unsuccessful password change. Try again.");
            }

            var tokenBytes = WebEncoders.Base64UrlDecode(resetRequest.Token);
            var decodedToken = Encoding.UTF8.GetString(tokenBytes);

            var result = await _userManager.ResetPasswordAsync(user, decodedToken, resetRequest.NewPassword);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new BadRequestException($"Unsuccessful password change: {errors}");
            }
        }
    }
}
