using InstaClone.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Domain.Interfaces
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        Task<IReadOnlyList<Post>> GetUserFeedAsync(Guid userId);
        Task<IReadOnlyList<Post>> GetPostsByUserAsync(Guid userId);
        Task<IReadOnlyList<Guid>> FilterLikedByUser(Guid userId, IReadOnlyList<Post> posts);
    }
}
