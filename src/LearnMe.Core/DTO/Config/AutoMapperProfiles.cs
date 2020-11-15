using AutoMapper;
using LearnMe.Core.DTO.Calendar;
using LearnMe.Core.DTO.HomeDTO;
using LearnMe.Core.DTO.User;
using LearnMe.Core.DTO.Account;
using LearnMe.Infrastructure.Models.Domains.Calendar;
using LearnMe.Infrastructure.Models.Domains.Home;
using LearnMe.Infrastructure.Models.Domains.Users;
using LearnMe.Core.DTO.Lessons;
using LearnMe.Infrastructure.Models.Domains.Lessons;
using System.Collections.Generic;
using LearnMe.Infrastructure.Models.Domains.Messages;
using LearnMe.Core.DTO.Messages;

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
            CreateMap<UserBasic, UserForMentorDto>().ReverseMap();
            CreateMap<UserBasic, RegisterFromMentor>().ReverseMap();
            CreateMap<UserBasic, UpdateUserDto>().ReverseMap();

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

            CreateMap<Lesson, LessonDto>();
            CreateMap<LessonDto, Lesson>();

            CreateMap<Message, MessageToReturnDto>()
                .ForMember(m => m.SenderName, opt => opt.MapFrom(u => u.Sender.FirstName))
                .ForMember(m => m.RecipientName, opt => opt.MapFrom(u => u.Sender.LastName));
        }
    }
}
