using AutoMapper;
using LearnMe.Core.DTO.User;
using LearnMe.Infrastructure.Models.Domains.Users;

namespace LearnMe.Infrastructure
{
    /* tutaj tworzymy mapy, tylko właściwości
       z takimi samymi nazwami zostaną zmapowane */
  
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // mapujemy z klasy UserBasic do klasy UserBasicDto
            CreateMap<UserBasic, UserBasicDto>();
            CreateMap<UserGroup, UserGroupDto>();
            CreateMap<UserInvoiceData, UserInvoiceDataDto>();
            CreateMap<UserLogin, UserLoginDto>();
            CreateMap<UserRegistration, UserRegistrationDto>();
        }
    }
}
