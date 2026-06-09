using AutoMapper;
using InstaClone.Application.DTOs.UserDTOs;
using InstaClone.Application.Exceptions;
using InstaClone.Application.Interfaces;
using InstaClone.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<UserDto>> GetAllAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            return _mapper.Map<IReadOnlyList<UserDto>>(users);
        }

        public async Task<UserDto?> GetByIdAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
                throw new NotFoundException("User was not found.");

            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public async Task<UserDto?> UpdateAsync(Guid id, UserUpdateRequestDto userData)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                throw new NotFoundException("User was not found.");

            _mapper.Map(userData, user);
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new BadRequestException("Saving user data changes failed.");
            }
            return _mapper.Map<UserDto>(user);
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                throw new NotFoundException("User was not found.");

            await _userManager.DeleteAsync(user);
        }
    }
}
