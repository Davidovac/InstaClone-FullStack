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
using System.Security.Claims;
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

        public async Task<UserDto> GetByIdAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
                throw new NotFoundException("User was not found.");

            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public async Task<IReadOnlyList<UserSimpleDto>> GetFollowersByUser(Guid userId)
        {
            var user = await _userManager.Users
                .Include(u => u.Followers)
                .SingleOrDefaultAsync(u => u.Id == userId);
            if (user == null)
                throw new NotFoundException("User was not found.");

            return _mapper.Map<IReadOnlyList<UserSimpleDto>>(user.Followers);
        }

        public async Task<IReadOnlyList<UserSimpleDto>> GetFollowingByUser(Guid userId)
        {
            var user = await _userManager.Users
                .Include(u => u.Following)
                .SingleOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                throw new NotFoundException("User was not found.");

            return _mapper.Map<IReadOnlyList<UserSimpleDto>>(user.Following);
        }

        public async Task<ProfileDto> GetProfileAsync(string userName)
        {
            var user = await _userManager.Users
                .Include(u => u.Followers)
                .Include(u => u.Following)
                .Include(u => u.Posts)
                .SingleOrDefaultAsync(u => u.UserName == userName);

            if (user == null)
                throw new NotFoundException("User was not found.");

            var userDto = _mapper.Map<ProfileDto>(user);

            userDto.PostsCount = user.Posts.Count();
            userDto.FollowerCount = user.Followers.Count();
            userDto.FollowingCount = user.Following.Count();
            return userDto;
        }

        public async Task FollowProfileAsync(string userName, ClaimsPrincipal claimsPrincipal)
        {
            try
            {
                var isFollowed = await FollowedProfileCheckAsync(userName, claimsPrincipal);
                if (isFollowed)
                {
                    throw new BadRequestException("Can't follow followed profile");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            var profile = await _userManager.FindByNameAsync(userName);
            var user = await _userManager.GetUserAsync(claimsPrincipal);

            user!.Following.Add(profile!);
            await _userManager.UpdateAsync(user);
        }

        public async Task UnfollowProfileAsync(string userName, ClaimsPrincipal claimsPrincipal)
        {
            try
            {
                var isFollowed = await FollowedProfileCheckAsync(userName, claimsPrincipal);
                if (!isFollowed)
                {
                    throw new BadRequestException("Can't unfollow not followed profile");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            var profile = await _userManager.FindByNameAsync(userName);
            var user = await _userManager.GetUserAsync(claimsPrincipal);

            var fullUser = await _userManager.Users
                .Include(u => u.Following)
                .SingleOrDefaultAsync(u => u.Id == user!.Id);

            user!.Following.Remove(profile!);
            await _userManager.UpdateAsync(user);
        }

        public async Task<bool> FollowedProfileCheckAsync(string userName, ClaimsPrincipal claimsPrincipal)
        {
            var profile = await _userManager.FindByNameAsync(userName);

            if (profile == null)
                throw new NotFoundException("Profile was not found.");

            var user = await _userManager.GetUserAsync(claimsPrincipal);
            if (user == null)
                throw new NotFoundException("User was not found.");

            var userWithFollowing = await _userManager.Users
                .Include(u => u.Following)
                .SingleOrDefaultAsync(u => u.Id == user.Id);

            if (user.Following.Any(f => f.Id == profile.Id))
            {
                return true;
            }
            return false;
        }

        public async Task<UserDto> GetByClaims(ClaimsPrincipal claimsPrincipal)
        {
            var user = await _userManager.GetUserAsync(claimsPrincipal);
            if (user == null)
                throw new UnauthorizedAccessException();

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> UpdateAsync(Guid id, UserUpdateRequestDto userData)
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

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                throw new BadRequestException("User deletion failed.");
            }
        }

    }
}