using AutoMapper;
using LearnMe.DTO;
using LearnMe.Models.Domains.Users;

namespace LearnMe.Helpers
{
    /* tutaj tworzymy mapy, tylko właściwości
       z takimi samymi nazwami zostaną zmapowane */
  
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // mapujemy z klasy UserBasic do klasy UserBasicList
            CreateMap<UserBasic, UserBasicList>(); 
        }
    }
}
