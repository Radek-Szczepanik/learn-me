﻿using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using LearnMe.Core.DTO.User;
using LearnMe.Core.Interfaces.DTO;
using LearnMe.Infrastructure.Models.Domains.Users;


namespace LearnMe.Core.DTO.Config
{
    public class RepositoryMapper<T> : IRepositoryMapper<T> where T : class
    {
        private readonly IMapper _mapper;

        public RepositoryMapper(IMapper mapper)
        {
            _mapper = mapper;

        }

        public UserBasic UserDtoMapper(T obj)
        {
            if (obj.GetType() == typeof(UserBasicDto))
            {
                var user = _mapper.Map<UserBasic>(obj);
                return user;
            }
            return null;
        }
    }
}