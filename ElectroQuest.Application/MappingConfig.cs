using AutoMapper;
using ElectroQuest.Application.Analytics.DTO;
using ElectroQuest.Application.Users.DTO;
using ElectroQuest.Domain.Entities;

namespace ElectroQuest.Application
{
    public  class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<RowData, GAPSICombinedDto>().ReverseMap();
            CreateMap<TotalAcrossAllPagesAndDatesDto, DailyStats>().ReverseMap();
            CreateMap<RowData , TotalPerPageDto>().ReverseMap();
            CreateMap<User, UserRegisterDto>().ReverseMap();
        }
    }
}
