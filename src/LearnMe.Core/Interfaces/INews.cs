using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;



namespace LearnMe.Core.Interfaces
{
    public interface INews
    {
        // public Task<List<News>> GetAll();


        Task<List<T>> ListAsync<T>();
    }
}