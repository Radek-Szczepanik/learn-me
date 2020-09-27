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

        public async Task<bool> DeleteAsync(object id)
        {
            var toBeDeleted = await _context.FindAsync<T>(id);
            _context.Remove(toBeDeleted);

            return await SaveAsync();
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>()
                                 .AsNoTracking()
                                 .ToListAsync();
        }

        public async Task<T> GetByIdAsync(object id)
        {
            var found = await _context.FindAsync<T>(id);
            _context.Entry(found).State = EntityState.Detached;

            return found;
        }

        public async Task<bool> InsertAsync(T obj)
        {
            await _context.AddAsync<T>(obj);
            
            return await SaveAsync();
        }

        public async Task<bool> SaveAsync()
        {
            var rowsAffected = await _context.SaveChangesAsync();

            return rowsAffected >= 1;
        }

        public async Task<bool> UpdateAsync(T obj)
        {
            _context.Update(obj);

            return await SaveAsync();
        }
    }
}
