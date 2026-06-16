using InstaClone.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Domain.Interfaces
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        Task<IReadOnlyList<Comment>> GetPostCommentsAsync(Guid postId);
        Task<IReadOnlyList<ReplyComment>> GetCommentRepliesAsync(Guid commentId);
    }
}
