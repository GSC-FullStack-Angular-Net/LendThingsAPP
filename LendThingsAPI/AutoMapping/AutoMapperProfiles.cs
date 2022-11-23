using AutoMapper;
using LendThingsAPI.Proto;
using LendThingsCommonClasses.DTO;
using LendThingsCommonClasses.Models;

namespace LendThingsAPI.AutoMapping
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles():base()
        {
            CreateMap<CategoryForCreationDTO, Category>();
            CreateMap<CategoryFullDTO, Category>().ReverseMap();
            CreateMap<PersonForCreationDTO, Person>();
            CreateMap<ThingForCreationDTO, Thing>().ForMember(t => t.Category, option => option.Ignore()).ReverseMap();

            CreateMap<ThingBaseDTO, Thing>().ReverseMap().ForMember(ori=>ori.Category, opt=>opt.MapFrom(dest=>dest.Category.Id));
            CreateMap<ThingFullDTO, Thing>().ReverseMap();

        }

    }
}
