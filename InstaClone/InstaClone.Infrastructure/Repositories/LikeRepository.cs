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
    public class LikeRepository : GenericRepository<Like>, ILikeRepository
    {
        public LikeRepository(AppDbContext dbContext) : base(dbContext) {}

        public async Task<Like?> GetOneByUserAndPostAsync(Guid postId, Guid userId)
        {
            return await _dbContext.Likes
                .Where(l => l.PostId == postId && l.LikerId == userId)
                .FirstOrDefaultAsync();
        }
    }

}
