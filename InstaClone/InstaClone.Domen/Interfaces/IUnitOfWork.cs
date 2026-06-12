using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IPostRepository Posts { get; }
        ILikeRepository Likes { get; }
        ICommentRepository Comments { get; }
        IReplyCommentRepository ReplyComments { get; }
        Task<int> CompleteAsync();
    }
}
