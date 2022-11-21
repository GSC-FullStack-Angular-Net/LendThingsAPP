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

            CreateMap<ThingForCreationDTO, ThingForCreationViewModel>().ReverseMap(); 
            CreateMap<ThingForCreationViewModel, ThingFullDTO>()
                .ReverseMap()
                .ForMember(src=> src.Category,opt=>opt.MapFrom(dto=>dto.Category.Id));
            CreateMap<ThingForCreationViewModel, ThingBaseDTO>();
            CreateMap<ThingFullDTO, ThingViewModel>().ReverseMap();
            CreateMap<CategoryFullDTO, CategoryViewModel>().ReverseMap();

        }
    }
}
