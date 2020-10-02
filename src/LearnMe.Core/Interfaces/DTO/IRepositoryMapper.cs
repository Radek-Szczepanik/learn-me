using System;
using System.Collections.Generic;
using System.Text;
using LearnMe.Infrastructure.Models.Domains.Users;
using LearnMe.Core.DTO.User;

namespace LearnMe.Core.Interfaces.DTO
{
    public interface IRepositoryMapper<T> where T : class
    {
        UserBasic UserDtoMapper(T obj);
        IEnumerable<UserBasicDto> UserDtoMapperGetAll(IEnumerable<UserBasic> obj);
    }
}