using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearnMe.Infrastructure.Repository.Interfaces
{
    public interface ICrudRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(int itemsPerPage, int pageNumber);

        Task<T> GetByIdAsync(object id);
        
        Task<T> InsertAsync(T obj);
        
        Task<bool> UpdateAsync(T obj);

        Task<bool> DeleteAsync(object id);

        Task<bool> SaveAsync();
    }
}