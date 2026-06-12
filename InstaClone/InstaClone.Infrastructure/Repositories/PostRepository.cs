using InstaClone.Application.DTOs.PostDTOs;
using InstaClone.Domain.Entities;
using InstaClone.Domain.Interfaces;
using InstaClone.Infrastructure.Persistence;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Infrastructure.Repositories
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        public PostRepository(AppDbContext dbContext) : base(dbContext) { }

        public async Task<IReadOnlyList<Post>> GetUserFeedAsync(Guid userId)
        {
            return await _dbContext.Users
                .Where(u => u.Id == userId)
                .SelectMany(u => u.Following)
                .SelectMany(f => f.Posts)
                .Include(p => p.Comments)
                .ThenInclude(c => c.ReplyComments)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Post>> GetPostsByUserAsync(Guid userId)
        {
            return await _dbContext.Users
                .Where(u => u.Id == userId)
                .SelectMany(u => u.Posts)
                .Include(p => p.Comments)
                .ThenInclude(c => c.ReplyComments)
                .OrderByDescending(u => u.CreatedAt)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Guid>> FilterLikedByUser(Guid userId, IReadOnlyList<Post> posts)
        {
            return await _dbContext.Posts
                .Where(p => 
                posts.Select(po => po.Id).Contains(p.Id) 
                && p.Likes.Select(l => l.LikerId).Contains(userId))
                .Select(p => p.Id)
                .ToListAsync();
        }
    }

}
