using System;
using System.Collections.Generic;
using System.Text;
using LearnMe.Infrastructure.Models.Domains.Users;

namespace LearnMe.Core.Interfaces.DTO
{
    public interface IRepositoryMapper<T> where T : class
    {
        UserBasic UserDtoMapper(T obj);
    }
}
