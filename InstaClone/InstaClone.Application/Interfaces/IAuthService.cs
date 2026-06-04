using InstaClone.Application.DTOs.AuthDTOs;
using InstaClone.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Application.Interfaces
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterRequestDto registerRequest);
        Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequest);
        Task ActivateAccountAsync(ActivateAccountRequestDto activateRequest);
    }
}
