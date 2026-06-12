using InstaClone.Application.DTOs.LikeDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Application.Interfaces
{
    public interface ILikeService
    {
        Task<IReadOnlyList<LikeResponseDto>> GetAllAsync();
        Task<LikeResponseDto?> GetOneAsync(Guid id);
        Task CreateAsync(LikeResponseDto likeDto);
        Task UpdateAsync(LikeResponseDto likeDto);
        Task DeleteAsync(Guid id);
    }
}
