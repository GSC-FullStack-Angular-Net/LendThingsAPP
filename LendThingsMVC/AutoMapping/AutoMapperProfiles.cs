using AutoMapper;
using LendThingsAPI.Models;
using LendThingsMVC.Models;

namespace LendThingsAPI.AutoMapping
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles():base()
        {
            CreateMap<Thing, ThingViewModel>();
        }
    }
}
