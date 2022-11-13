using AutoMapper;
using LendThingsAPI.DTO;
using LendThingsAPI.Models;

namespace LendThingsAPI.AutoMapping
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles():base()
        {
            CreateMap<CategoryForCreationDTO, Category>();
        }
    }
}
