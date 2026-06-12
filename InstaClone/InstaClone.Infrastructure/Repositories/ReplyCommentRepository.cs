using InstaClone.Domain.Entities;
using InstaClone.Domain.Interfaces;
using InstaClone.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Infrastructure.Repositories
{
    public class ReplyCommentRepository : GenericRepository<ReplyComment>, IReplyCommentRepository
    {
        public ReplyCommentRepository(AppDbContext dbContext) : base(dbContext) {}
    }
}
