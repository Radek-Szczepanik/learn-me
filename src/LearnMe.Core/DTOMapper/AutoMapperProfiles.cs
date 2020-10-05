using AutoMapper;
using LearnMe.Core.DTO.Calendar;
using LearnMe.Core.DTO.HomeDTO;
using LearnMe.Core.DTO.User;
using LearnMe.Infrastructure.Models.Domains.Calendar;
using LearnMe.Infrastructure.Models.Domains.Home;
using LearnMe.Infrastructure.Models.Domains.Users;

namespace LearnMe.Core.DTOMapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserBasic, UserBasicDto>();
            CreateMap<UserGroup, UserGroupDto>();
            CreateMap<UserInvoiceData, UserInvoiceDataDto>();
            CreateMap<UserLogin, UserLoginDto>();
            CreateMap<UserRegistration, UserRegistrationDto>();

            CreateMap<CalendarEvent, CalendarEventDto>();
            CreateMap<CalendarEventDto, CalendarEvent>();

            CreateMap<Exercises, ExercisesDTO>();
            CreateMap<News, NewsDTO>();
            CreateMap<Opinion, OpinionDTO>();
            CreateMap<Portfolio, PortfolioDTO>();
            CreateMap<Question, QuestionDTO>();
            CreateMap<TutorService, TutorServiceDTO>();
        }
    }
}
