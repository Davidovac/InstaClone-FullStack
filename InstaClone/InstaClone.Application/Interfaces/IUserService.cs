using InstaClone.Application.DTOs;
using InstaClone.Application.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Application.Interfaces
{
    public interface IUserService
    {
        Task<IReadOnlyList<UserDto>> GetAllAsync();
        Task<UserDto> GetByIdAsync(Guid id);
        Task<UserDto> GetByClaims(ClaimsPrincipal claimsPrincipal);
        Task<UserDto> UpdateAsync(Guid id, UserUpdateRequestDto userData);
        Task DeleteAsync(Guid id);
    }
}
