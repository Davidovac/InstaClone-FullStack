using InstaClone.Application.DTOs.CommentDTOs;
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
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(AppDbContext dbContext) : base(dbContext) {}
        public async Task<IReadOnlyList<Comment>> GetPostCommentsAsync(Guid postId)
        {
            return await _dbContext.Posts
                .Where(p => p.Id == postId)
                .SelectMany(c => c.Comments)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<ReplyComment>> GetCommentRepliesAsync(Guid commentId)
        {
            return await _dbContext.Comments
                .Where(c => c.Id == commentId)
                .SelectMany(c => c.ReplyComments)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }
    }

}
