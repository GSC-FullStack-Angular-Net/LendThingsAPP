using AutoMapper;
using LendThingsCommonClasses.DTO;
using LendThingsCommonClasses.Models;

namespace LendThingsAPI.AutoMapping
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles():base()
        {
            CreateMap<CategoryForCreationDTO, Category>();
            CreateMap<PersonForCreationDTO, Person>();
            CreateMap<ThingForCreationDTO, Thing>().ForMember(t => t.Category, option => option.Ignore()).ReverseMap();
        }

    }
}
