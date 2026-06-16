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
        private readonly IUserService _userService;
        private readonly IPostService _postService;

        public CommentService(IUnitOfWork unitOfWork, IMapper mapper, IUserService userService, IPostService postService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userService = userService;
            _postService = postService;
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

        public async Task<CommentResponseDto> GetOneAsync(Guid id)
        {
            var comment = await _unitOfWork.Comments.GetOneAsync(id);
            if (comment == null)
                throw new NotFoundException("Comment with id:" + id.ToString());
            return _mapper.Map<CommentResponseDto>(comment);
        }

        public async Task<CommentResponseDto> CommentOnPostAsync(Guid postId, CommentCreateRequestDto comment, ClaimsPrincipal claimsPrincipal)
        {
            var user = await _userService.GetByClaims(claimsPrincipal);
            var post = await _postService.GetOneAsync(postId);

            var newComment = _mapper.Map<Comment>(comment);
            newComment.PostId = post.Id;
            newComment.AuthorId = user.Id;

            await _unitOfWork.Comments.AddAsync(newComment);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<CommentResponseDto>(newComment);
        }
        public async Task<ReplyResponseDto> ReplyOnCommentOnThisPostAsync(Guid postId, Guid commentId, ReplyCreateRequestDto reply, ClaimsPrincipal claimsPrincipal)
        {
            var user = await _userService.GetByClaims(claimsPrincipal);
            await _postService.GetOneAsync(postId);
            var comment = await GetOneAsync(commentId);

            if (comment.PostId != postId)
            {
                throw new BadRequestException("Comment and post do not match");
            }

            var newReply = _mapper.Map<ReplyComment>(reply);
            newReply.RepliedCommentId = commentId;
            newReply.AuthorId = user.Id;

            await _unitOfWork.ReplyComments.AddAsync(newReply);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<ReplyResponseDto>(newReply);
        }

        public async Task CreateAsync(CommentCreateRequestDto commentDto, ClaimsPrincipal claimsPrincipal)
        {
            var user = await _userService.GetByClaims(claimsPrincipal);

            var comment = _mapper.Map<Comment>(commentDto);
            comment.AuthorId = user.Id;
            await _unitOfWork.Comments.AddAsync(comment);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateAsync(Guid id, CommentUpdateRequestDto commentDto, ClaimsPrincipal claimsPrincipal)
        {
            var user = await _userService.GetByClaims(claimsPrincipal);
            var comment = await _unitOfWork.Comments.GetOneAsync(id);

            if (comment.AuthorId != user.Id)
                throw new UnauthorizedAccessException();

            _mapper.Map(commentDto, comment);
            _unitOfWork.Comments.Update(comment);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(Guid id, ClaimsPrincipal claimsPrincipal)
        {
            var userDto = await _userService.GetByClaims(claimsPrincipal);
            var comment = await _unitOfWork.Comments.GetOneAsync(id);

            if (comment.AuthorId != userDto.Id)
                throw new UnauthorizedAccessException();

            _unitOfWork.Comments.Delete(comment);
            await _unitOfWork.CompleteAsync();
        }

    }
}