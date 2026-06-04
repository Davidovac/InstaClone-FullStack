using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetOneAsync(Guid id);
        Task<T> AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Dispose();
    }
}
