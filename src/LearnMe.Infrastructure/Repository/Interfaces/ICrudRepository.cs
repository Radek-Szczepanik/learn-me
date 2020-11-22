using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearnMe.Infrastructure.Repository.Interfaces
{
    public interface ICrudRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        
        Task<IEnumerable<T>> GetAllWithPagination(int itemsPerPage = 10, int pageNumber = 1);
        Task<T> GetByIdAsync(object id);
        
        Task<T> InsertAsync(T entity);
        
        Task<bool> UpdateAsync(T entity);

        Task<bool> DeleteAsync(object id);

        Task<bool> SaveAsync();
    }
}