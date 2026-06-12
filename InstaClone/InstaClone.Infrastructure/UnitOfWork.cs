using InstaClone.Domain.Interfaces;
using InstaClone.Infrastructure.Persistence;
using InstaClone.Infrastructure.Repositories;
using Microsoft.AspNetCore.DataProtection.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace InstaClone.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        public IPostRepository Posts { get; private set; }
        public IReplyCommentRepository ReplyComments { get; private set; }
        public ILikeRepository Likes { get; private set; }
        public ICommentRepository Comments { get; private set; }

        public UnitOfWork(AppDbContext dbContext)
        {

            _dbContext = dbContext;
            Posts = new PostRepository(_dbContext);
            Likes = new LikeRepository(_dbContext);
            Comments = new CommentRepository(_dbContext);
            ReplyComments = new ReplyCommentRepository(_dbContext);
        }

        public Task<int> CompleteAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
