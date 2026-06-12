using InstaClone.Application.DTOs.CommentDTOs;
using InstaClone.Application.DTOs.LikeDTOs;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InstaClone.Application.Interfaces
{
    public interface ICommentService
    {
        Task<IReadOnlyList<CommentResponseDto>> GetPostCommentsAsync(Guid postId);
        Task<IReadOnlyList<CommentResponseDto>> GetAllAsync();
        Task<IReadOnlyList<ReplyResponseDto>> GetCommentRepliesAsync(Guid commentId);
        Task<CommentResponseDto?> GetOneAsync(Guid id);
        Task CreateAsync(CommentCreateRequestDto commentDto, ClaimsPrincipal user);
        Task UpdateAsync(Guid id, CommentUpdateRequestDto commentDto, ClaimsPrincipal user);
        Task DeleteAsync(Guid id, ClaimsPrincipal user);
    }
}
