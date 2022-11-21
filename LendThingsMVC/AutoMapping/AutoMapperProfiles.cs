using AutoMapper;
using LendThingsCommonClasses.DTO;
using LendThingsMVC.Models;
using LendThingsMVC.Services;

namespace LendThingsMVC.AutoMapping
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() : base()
        {
            CreateMap<CategoryFullDTO, CategoryViewModel>().ReverseMap();

            CreateMap<ThingForCreationDTO, ThingForCreationViewModel>().ReverseMap(); 
            CreateMap<ThingForCreationViewModel, ThingFullDTO>()
                .ReverseMap()
                .ForMember(src=> src.Category,opt=>opt.MapFrom(dto=>dto.Category.Id));
            CreateMap<ThingForCreationViewModel, ThingBaseDTO>().ReverseMap()
                .ForMember(src => src.Category, opt => opt.MapFrom(dto => dto.Category)); ;
            CreateMap<ThingFullDTO, ThingViewModel>().ReverseMap();
            CreateMap<ThingFullDTO, ThingBaseDTO>()
                .ForMember(src => src.Category, opt=>opt.MapFrom(dest=>dest.Category.Id))
                .ReverseMap();
            ;

        }
    }
}
