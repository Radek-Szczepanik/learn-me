using LearnMe.Infrastructure.Models.Domains.Messages;
using LearnMe.Infrastructure.Models.Domains.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearnMe.Infrastructure.Repository.Interfaces
{
    public interface ICrudRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        
        Task<IEnumerable<T>> GetAllWithPagination(int itemsPerPage = 10, int pageNumber = 1);
        Task<T> GetByIdAsync(object id);
        
        Task<T> InsertAsync(T obj);
        
        Task<bool> UpdateAsync(T obj);

        Task<bool> DeleteAsync(object id);

        Task<bool> SaveAsync();

    }
}