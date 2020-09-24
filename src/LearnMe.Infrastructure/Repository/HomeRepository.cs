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
    public class HomeRepository : INews

    {
        ApplicationDbContext _context;

        public HomeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //public async Task<IEnumerable<News>> GetAll()
        //{
           
        //}

        public Task<List<T>> ListAsync<T>()
        {
            return _context.News.ToListAsync;
        }
    }
}
