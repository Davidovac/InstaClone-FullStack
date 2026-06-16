using InstaClone.Application.DTOs.CommentDTOs;
using InstaClone.Application.DTOs.LikeDTOs;
using InstaClone.Application.DTOs.PostDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Application.Interfaces
{
    public interface IPostService
    {
        Task<IReadOnlyList<PostSummaryResponseDto>> GetUserFeedAsync(ClaimsPrincipal claimsPrincipal);
        Task<IReadOnlyList<PostSummaryResponseDto>> GetByUserAsync(ClaimsPrincipal claimsPrincipal);
        Task<PostDetailResponseDto?> GetOneAsync(Guid id);
        Task CreateAsync(PostCreateRequestDto postDto, IFormFile? file, ClaimsPrincipal claimsPrincipal);
        Task UpdateAsync(PostUpdateRequestDto postDto, ClaimsPrincipal claimsPrincipal);
        Task DeleteAsync(Guid id, ClaimsPrincipal claimsPrincipal);
    }
}
