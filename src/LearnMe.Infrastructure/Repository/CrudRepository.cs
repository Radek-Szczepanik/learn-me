﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnMe.Infrastructure.Data;
using LearnMe.Infrastructure.Models.Domains.Users;
using LearnMe.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

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

            if (toBeDeleted != null)
            {

                _context.Remove(toBeDeleted);

                return await SaveAsync();
            }
            else
            {
                return false;
            }
        }
        public async Task<IEnumerable<T>> GetAllAsync(int itemsPerPage, int pageNumber)
        {
            if (itemsPerPage > 0 && pageNumber > 0)
            {
                return await _context.Set<T>()
                                                 .Skip((pageNumber - 1) * itemsPerPage)
                                                 .Take(itemsPerPage)
                                                 .AsNoTracking()
                                                 .ToListAsync();
            } else
            {
                return null;
            }

        }

        public async Task<T> GetByIdAsync(object id)
        {
            var found = await _context.FindAsync<T>(id);

            if (found != null)
            {
                _context.Entry(found).State = EntityState.Detached;

                return found;
            } else
            {
                return null;
            }
        }

        public async Task<T> InsertAsync(T obj)
        {
            var inserted = await _context.AddAsync<T>(obj);
            bool isSuccess = await SaveAsync();

            var newEvent = inserted.Entity;

            return newEvent ?? null;
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
