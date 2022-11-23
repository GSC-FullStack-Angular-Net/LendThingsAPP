using LendThingsCommonClasses.DTO;
using LendThingsMVC.Configuration;
using LendThingsMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LendThingsMVC.Services
{
    public class ThingService : BaseModelService<ThingBaseDTO, ThingFullDTO, ThingForCreationDTO, ThingBaseDTO, ThingBaseDTO>, IThingService
    {
        public ThingService(IOptions<APIOptions> options) : base(options)
        {
        }

        public override Task<ProcesedResponse<List<ThingBaseDTO>>> GetAllBaseAsync()
        {
            throw new NotImplementedException();
        }

        async public override Task<ProcesedResponse<List<ThingFullDTO>>> GetAllFullAsync()
        {
            var response = await DoGetRequestOn("Thing");
            return await ProcessResponse<List<ThingFullDTO>>(response);
        }

        public override bool Exists(int id)
        {
            throw new NotImplementedException();
        }

        async public override Task<ProcesedResponse<ThingFullDTO>> GetByIdAsync(int id)
        {
            var response = await DoGetRequestOn($"Thing/{id}");
            return await ProcessResponse<ThingFullDTO>(response);

        }

        public override async Task<ProcesedResponse<ThingBaseDTO>> UpdateAsync(ThingBaseDTO entity)
        {
            var response = await DoPatchRequestFor<ThingBaseDTO>($"Thing/{entity.Id}",entity);
            return await ProcessResponse<ThingBaseDTO>(response);
        }

        async public override Task<ProcesedResponse<string>> DeleteAsync(ThingBaseDTO entity)
        {
            var response = await DoDeleteRequestFor<string>($"thing/{entity.Id}");
            return await ProcessResponse<string>(response);
        }

        async public override Task<ProcesedResponse<ThingBaseDTO>> SaveAsync(ThingForCreationDTO entity)
        {
            var response = await DoPostRequestFor<ThingBaseDTO>($"Thing/Create",entity);
            return await ProcessResponse<ThingBaseDTO>(response);
        }

    }
}
