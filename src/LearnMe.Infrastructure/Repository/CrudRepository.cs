using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LearnMe.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using LearnMe.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using LearnMe.Infrastructure.Models.Domains.Home;

namespace LearnMe.Infrastructure.Repository
{
    public class CrudRepository<T> : ICrudRepository<T> where T: class

    {
        private readonly ApplicationDbContext _context;

        public CrudRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public T GetById(object id)
        {
            throw new NotImplementedException();
        }

        public void Insert(T obj)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(T obj)
        {
            throw new NotImplementedException();
        }
    }
}
