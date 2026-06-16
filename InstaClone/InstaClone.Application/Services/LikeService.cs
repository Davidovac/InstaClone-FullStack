using AutoMapper;
using InstaClone.Application.DTOs.LikeDTOs;
using InstaClone.Application.Exceptions;
using InstaClone.Application.Interfaces;
using InstaClone.Domain.Entities;
using InstaClone.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Application.Services
{
    public class LikeService : ILikeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        private readonly IPostService _postService;
        private readonly IMapper _mapper;

        public LikeService(IUnitOfWork unitOfWork, IMapper mapper, IUserService userService, IPostService postService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userService = userService;
            _postService = postService;
        }

        public async Task<IReadOnlyList<LikeResponseDto>> GetAllAsync()
        {
            return _mapper.Map<IReadOnlyList<LikeResponseDto>>(await _unitOfWork.Likes.GetAllAsync());
        }

        public async Task<LikeResponseDto?> GetOneAsync(Guid id)
        {
            var like = await _unitOfWork.Likes.GetOneAsync(id);
            if (like == null)
            {
                throw new NotFoundException(id.ToString());
            }
            return _mapper.Map<LikeResponseDto>(like);
        }

        public async Task LikePostAsync(Guid postId, ClaimsPrincipal claimsPrincipal)
        {
            var userDto = await _userService.GetByClaims(claimsPrincipal);
            var post = await _postService.GetOneAsync(postId);

            Like like = new Like
            {
                PostId = post.Id,
                LikerId = userDto.Id
            };

            await _unitOfWork.Likes.AddAsync(like);
            await _unitOfWork.CompleteAsync();
        }
        public async Task UnlikePostAsync(Guid postId, ClaimsPrincipal claimsPrincipal)
        {
            var user = await _userService.GetByClaims(claimsPrincipal);
            var post = await _unitOfWork.Posts.GetOneAsync(postId);
            var like = await _unitOfWork.Likes.GetOneByUserAndPostAsync(postId, user.Id);

            if (like == null)
                throw new NotFoundException("Like");

            if (like.LikerId != user.Id)
                throw new BadRequestException("Liker id and user id do not match.");

            if (like.PostId != post.Id)
                throw new BadRequestException("Like's post id do not match given post id.");

            post.Likes.Remove(like);
            _unitOfWork.Likes.Delete(like);
            await _unitOfWork.CompleteAsync();
        }

        public async Task CreateAsync(LikeResponseDto likeDto)
        {
            var like = _mapper.Map<Like>(likeDto);
            await _unitOfWork.Likes.AddAsync(like);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateAsync(LikeResponseDto likeDto)
        {
            var like = await _unitOfWork.Likes.GetOneAsync(likeDto.Id);
            _mapper.Map(likeDto, like);
            _unitOfWork.Likes.Update(like);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var like = await _unitOfWork.Likes.GetOneAsync(id);
            _unitOfWork.Likes.Delete(like);
            await _unitOfWork.CompleteAsync();
        }

    }
}
