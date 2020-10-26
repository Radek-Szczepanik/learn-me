using AutoMapper;
using LearnMe.Core.DTO.Calendar;
using LearnMe.Core.DTO.HomeDTO;
using LearnMe.Core.DTO.User;
using LearnMe.Core.DTO.Account;
using LearnMe.Infrastructure.Models.Domains.Calendar;
using LearnMe.Infrastructure.Models.Domains.Home;
using LearnMe.Infrastructure.Models.Domains.Users;

using System.Collections.Generic;

namespace LearnMe.Core.DTO.Config
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserBasic, UserBasicDto>();
            CreateMap<UserBasic, RegisterDto>().ReverseMap();

            CreateMap<UserGroup, UserGroupDto>();
            CreateMap<UserInvoiceData, UserInvoiceDataDto>();
            CreateMap<UserBasic, LoginDto>();
            

            CreateMap<CalendarEvent, CalendarEventDto>()
                .ForMember(dest => dest.StartDate,
                    opt => opt.MapFrom(src => src.Start))
                .ForMember(dest => dest.EndDate,
                    opt => opt.MapFrom(src => src.End))
                .ForMember(dest => dest.Subject,
                opt => opt.MapFrom(src => src.Title));
            CreateMap<CalendarEventDto, CalendarEvent>()
                .ForMember(dest => dest.Start,
                    opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.End,
                    opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.Title,
                    opt => opt.MapFrom(src => src.Subject));

            CreateMap<Exercises, ExercisesDTO>();
            CreateMap<News, NewsDTO>();
            CreateMap<Opinion, OpinionDTO>();
            CreateMap<Portfolio, PortfolioDTO>();
            CreateMap<Question, QuestionDTO>();
            CreateMap<TutorService, TutorServiceDTO>();
        }
    }
}
