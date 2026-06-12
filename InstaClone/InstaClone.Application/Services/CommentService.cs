using AutoMapper;
using InstaClone.Application.DTOs.CommentDTOs;
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
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public CommentService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IReadOnlyList<CommentResponseDto>> GetPostCommentsAsync(Guid postId)
        {
            return _mapper.Map<IReadOnlyList<CommentResponseDto>>(await _unitOfWork.Comments.GetPostCommentsAsync(postId));
        }

        public async Task<IReadOnlyList<CommentResponseDto>> GetAllAsync()
        {
            return _mapper.Map<IReadOnlyList<CommentResponseDto>>(await _unitOfWork.Comments.GetAllAsync());
        }

        public async Task<IReadOnlyList<ReplyResponseDto>> GetCommentRepliesAsync(Guid commentId)
        {
            return _mapper.Map<IReadOnlyList<ReplyResponseDto>>(await _unitOfWork.Comments.GetCommentRepliesAsync(commentId));
        }

        public async Task<CommentResponseDto?> GetOneAsync(Guid id)
        {
            var comment = await _unitOfWork.Comments.GetOneAsync(id);
            if (comment == null)
            {
                throw new NotFoundException(id.ToString());
            }
            return _mapper.Map<CommentResponseDto>(comment);
        }

        public async Task CreateAsync(CommentCreateRequestDto commentDto, ClaimsPrincipal claimsPrincipal)
        {
            var user = await _userManager.GetUserAsync(claimsPrincipal);
            if (user == null)

                throw new UnauthorizedAccessException();
            var comment = _mapper.Map<Comment>(commentDto);
            comment.AuthorId = user.Id;
            await _unitOfWork.Comments.AddAsync(comment);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateAsync(Guid id, CommentUpdateRequestDto commentDto, ClaimsPrincipal claimsPrincipal)
        {
            var user = await _userManager.GetUserAsync(claimsPrincipal);
            if (user == null)

                throw new UnauthorizedAccessException();
            var comment = await _unitOfWork.Comments.GetOneAsync(id);
            if (comment == null)
            {
                throw new NotFoundException(id.ToString());
            }
            _mapper.Map(commentDto, comment);
            _unitOfWork.Comments.Update(comment);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(Guid id, ClaimsPrincipal claimsPrincipal)
        {
            var user = await _userManager.GetUserAsync(claimsPrincipal);
            if (user == null)
                throw new UnauthorizedAccessException();

            var comment = await _unitOfWork.Comments.GetOneAsync(id);
            if (comment == null)
            {
                throw new NotFoundException(id.ToString());
            }
            _unitOfWork.Comments.Delete(comment);
            await _unitOfWork.CompleteAsync();
        }

    }
}
