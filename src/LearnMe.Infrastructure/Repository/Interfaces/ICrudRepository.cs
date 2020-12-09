using LearnMe.Infrastructure.Models.Domains.Messages;
using LearnMe.Infrastructure.Models.Domains.Users;
using System.Collections.Generic;
using System.Threading.Tasks;
using LearnMe.Infrastructure.Models.Base;

namespace LearnMe.Infrastructure.Repository.Interfaces
{
    public interface ICrudRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        
        Task<IEnumerable<T>> GetAllWithPagination(int itemsPerPage = 10, int pageNumber = 1);
        Task<T> GetByIdAsync(object id);
        
        Task<T> InsertAsync(T entity);
        
        Task<bool> UpdateAsync(T entity);

        Task<bool> DeleteAsync(int id);

        Task<bool> DeleteAsync(T entity);

        Task<bool> SaveAsync();

    }
}