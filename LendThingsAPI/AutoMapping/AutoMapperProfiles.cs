using AutoMapper;
using LendThingsAPI.DTO;
using LendThingsAPI.Models;

namespace LendThingsAPI.AutoMapping
{
    public class AutoMapperProfiles : Profile
    {
        protected AutoMapperProfiles()
        {
            CreateMap<CategoryForCreationDTO, Category>();
        }
    }
}
