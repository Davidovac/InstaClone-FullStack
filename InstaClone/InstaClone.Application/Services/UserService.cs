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

            var response = new List<UserDto>();

            foreach (var u in users)
            {
                var roles = await _userManager.GetRolesAsync(u);
                response.Add(new UserDto
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Roles = roles
                });
            }

            return response;
        }

        public async Task<UserDto?> GetByIdAsync(Guid id)
        {
            var user = _userManager.FindByIdAsync(id.ToString());

            if (user == null)
                throw new NotFoundException("User was not found.");

            var userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }
    }
}
