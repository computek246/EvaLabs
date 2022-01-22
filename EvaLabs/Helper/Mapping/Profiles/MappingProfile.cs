using AutoMapper;
using EvaLabs.Domain.Entities;
using EvaLabs.ViewModels.Branch;
using EvaLabs.ViewModels.Test;

namespace EvaLabs.Helper.Mapping.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Test, TestViewModel>().ReverseMap();

            CreateMap<Branch, BranchViewModel>()
                .ForMember(e => e.AreaName, x => x.MapFrom(e => e.Area.AreaName))
                .ForMember(e => e.CityName, x => x.MapFrom(e => e.Area.City.CityName))
                .ReverseMap();
        }
    }
}
