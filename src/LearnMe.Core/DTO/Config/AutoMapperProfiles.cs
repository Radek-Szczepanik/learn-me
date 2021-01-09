using AutoMapper;
using LearnMe.Core.DTO.Calendar;
using LearnMe.Core.DTO.HomeDTO;
using LearnMe.Core.DTO.User;
using LearnMe.Core.DTO.Account;
using LearnMe.Core.DTO.Messages;
using LearnMe.Core.DTO.Lessons;
using LearnMe.Infrastructure.Models.Domains.Calendar;
using LearnMe.Infrastructure.Models.Domains.Home;
using LearnMe.Infrastructure.Models.Domains.Users;
using LearnMe.Infrastructure.Models.Domains.Lessons;
using System.Collections.Generic;
using LearnMe.Infrastructure.Models.Domains.Messages;

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
            CreateMap<MessageToCreateDto, Message>().ReverseMap();
            CreateMap<Message, MessageToReturnDto>();

            CreateMap<Lesson, LessonDto>();
            CreateMap<LessonDto, Lesson>();

            // ----------------------------
            CreateMap<UserLesson, UserBasicDto>()
                .ForMember(dest => dest.FirstName,
                    opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName,
                    opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.Email,
                    opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.PhoneNumber,
                    opt => opt.MapFrom(src => src.User.PhoneNumber));
            CreateMap<UserBasicDto, UserLesson>()
                .ForPath(dest => dest.User,
                    opt => opt.MapFrom(src => src.Email))
                .ForPath(dest => dest.User.FirstName,
                    opt => opt.MapFrom(src => src.FirstName))
                .ForPath(dest => dest.User.LastName,
                    opt => opt.MapFrom(src => src.LastName))
                .ForPath(dest => dest.User.Email,
                    opt => opt.MapFrom(src => src.Email))
                .ForPath(dest => dest.User.PhoneNumber,
                    opt => opt.MapFrom(src => src.PhoneNumber));

            CreateMap<UserBasicDto, UserBasic>();

            CreateMap<CalendarEvent, FullCalendarEventDto>()
                .ForMember(dest => dest.StartDate,
                    opt => opt.MapFrom(src => src.Start))
                .ForMember(dest => dest.EndDate,
                    opt => opt.MapFrom(src => src.End))
                .ForMember(dest => dest.Subject,
                    opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Attendees,
                opt => opt.MapFrom(src => src.Lesson.UserLessons));
            CreateMap<FullCalendarEventDto, CalendarEvent>()
                .ForMember(dest => dest.Start,
                    opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.End,
                    opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.Title,
                    opt => opt.MapFrom(src => src.Subject))
                .ForMember(dest => dest.Attendees,
                    opt => opt.MapFrom(src => src.Attendees));
                //.ForPath(dest => dest.Lesson.UserLessons,
                //    opt => opt.MapFrom(src => src.Attendees));
        }
    }
}
