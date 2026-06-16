using InstaClone.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Domain.Interfaces
{

    public interface ILikeRepository : IGenericRepository<Like>
    {
        Task<Like?> GetOneByUserAndPostAsync(Guid postId, Guid userId);
    }
}
