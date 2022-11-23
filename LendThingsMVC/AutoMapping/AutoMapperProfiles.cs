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
            CreateMap<CategoryFullDTO, CategoryViewModel>()
                .ReverseMap();

            CreateMap<ThingFullDTO, ThingBaseDTO>()
                .ForMember(src => src.Category, opt=>opt.MapFrom(dest=>dest.Category.Id))
                .ReverseMap();
            
            CreateMap<ThingForCreationDTO, ThingCreationViewModel>()
                .ReverseMap(); 
            CreateMap<ThingCreationViewModel, ThingFullDTO>()
                .ReverseMap()
                .ForMember(src=> src.Category,opt=>opt.MapFrom(dto=>dto.Category.Id));
            CreateMap<ThingCreationViewModel, ThingBaseDTO>()
                .ReverseMap()
                .ForMember(src => src.Category, opt => opt.MapFrom(dto => dto.Category)); 
            CreateMap<ThingEditViewModel, ThingBaseDTO>()
                .ReverseMap()
                .ForMember(src => src.Category, opt => opt.MapFrom(dto => dto.Category));
            CreateMap<ThingEditViewModel, ThingFullDTO>()
                .ReverseMap()
                .ForMember(src => src.Category, opt => opt.MapFrom(dto => dto.Category.Id));
            CreateMap<ThingFullDTO, ThingViewModel>().ReverseMap();
            
            ;
        }
    }
}
