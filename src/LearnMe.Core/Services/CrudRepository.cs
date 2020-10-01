using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnMe.Infrastructure.Data;
using LearnMe.Infrastructure.Models.Domains.Users;
using LearnMe.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using LearnMe.Core.DTO.User;
using LearnMe.Core.Interfaces.DTO;
using LearnMe.Core.DTO.Config;
using AutoMapper;


namespace LearnMe.Infrastructure.Repository
{
    public class CrudRepository<T> : ICrudRepository<T> where T : class

    {
        private readonly ApplicationDbContext _context;
        private readonly IRepositoryMapper<T> _mapper;



        public CrudRepository(ApplicationDbContext context, IRepositoryMapper<T> mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> DeleteAsync(object id)
        {
            var toBeDeleted = await _context.FindAsync<T>(id);

            if (toBeDeleted != null)
            {
                _context.Remove(toBeDeleted);

                return await SaveAsync();
            }
            else
            {
                // TODO How to make difference between id not found to not deleted
                // to return correct HTTP response 404 or we shall return 404 at all times
                // (either not successful delete or id not found ?)
                return false;
            }
        }
        public async Task<IEnumerable<T>> GetAllAsync(int itemsPerPage, int pageNumber)
        {
            return await _context.Set<T>()
                                 .Skip((pageNumber - 1) * itemsPerPage)
                                 .Take(itemsPerPage)
                                 .AsNoTracking()
                                 .ToListAsync();
        }

        public async Task<T> GetByIdAsync(object id)
        {
            var found = await _context.FindAsync<T>(id);

            if (found != null)
            {
                _context.Entry(found).State = EntityState.Detached;

                return found;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> InsertAsync(T obj)
        {
            if (obj.GetType() == typeof(UserLoginDto) || obj.GetType() == typeof(UserRegistrationDto))
            {
                //var user = _mapper.Map<UserBasic>(obj);
                await _context.AddAsync(_mapper.UserDtoMapper(obj));
            }
                    
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
