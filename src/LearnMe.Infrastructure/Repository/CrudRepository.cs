using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LearnMe.Infrastructure.Data;
using LearnMe.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LearnMe.Infrastructure.Repository
{
    public class CrudRepository<T> : ICrudRepository<T> where T : class

    {
        private readonly ApplicationDbContext _context;

        public CrudRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        Task<bool> ICrudRepository<T>.DeleteAsync(object id)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        Task<T> ICrudRepository<T>.GetByIdAsync(object id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertAsync(T obj)
        {
            await _context.AddAsync<T>(obj);
            var rowsAffected = await _context.SaveChangesAsync();

            return rowsAffected >= 1 ? true : false;
        }

        Task<bool> ICrudRepository<T>.SaveAsync()
        {
            throw new NotImplementedException();
        }

        Task<bool> ICrudRepository<T>.UpdateAsync(T obj)
        {
            throw new NotImplementedException();
        }
    }
}
