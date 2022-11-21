using LendThingsCommonClasses.DTO;
using LendThingsMVC.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LendThingsMVC.Services
{
    public class ThingService : BaseModelService<ThingBaseDTO, ThingFullDTO, ThingForCreationDTO, ThingBaseDTO, ThingBaseDTO>, IThingService
    {
        public ThingService(IOptions<APIOptions> options) : base(options)
        {
        }

        public override Task<List<ThingBaseDTO>> GetAllBaseAsync()
        {
            throw new NotImplementedException();
        }

        async public override Task<List<ThingFullDTO>> GetAllFullAsync()
        {
            var result =await DoGetRequestFor<List<ThingFullDTO>>("Thing");
            return result;
        }

        public override bool Exists(int id)
        {
            throw new NotImplementedException();
        }

        async public override Task<ThingFullDTO> GetByIdAsync(int id)
        {
            var result = await DoGetRequestFor<ThingFullDTO>($"Thing/{id}");
            return result;
        }

        public override async Task UpdateAsync(ThingBaseDTO entity)
        {
            var result = await DoPatchRequestFor<ThingBaseDTO>($"Thing/{entity.Id}",entity);
        }

        async public override Task DeleteAsync(ThingBaseDTO entity)
        {
            var result = await DoDeleteRequestFor<string>($"thing/{entity.Id}");
        }

        async public override Task SaveAsync(ThingForCreationDTO entity)
        {
            await DoPostRequestFor<ThingForCreationDTO>($"Thing/Create",entity);
        }
    }
}
