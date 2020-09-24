using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;



namespace LearnMe.Core.Interfaces
{
    public interface IHome<T> where T: class
    {
        // public Task<List<News>> GetAll();
        Task<IEnumerable<T>> GetAll();
        T GetById(object id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object id);
        void Save();
    }
}