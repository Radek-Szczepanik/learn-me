using AutoMapper;
using LearnMe.Core.DTO.Calendar;
using LearnMe.Core.DTO.User;
using LearnMe.Core.DTO.Account;
using LearnMe.Infrastructure.Models.Domains.Calendar;
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
            CreateMap<UserBasic, UserLoginDto>();
            CreateMap<UserBasic, RegisterDto>();

            CreateMap<CalendarEvent, CalendarEventDto>();
            CreateMap<CalendarEventDto, CalendarEvent>();
        }
    }
}
