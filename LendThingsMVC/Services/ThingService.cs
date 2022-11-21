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

        public override void Delete(ThingBaseDTO entity)
        {
            throw new NotImplementedException();
        }

        public override void DeleteAsync(ThingBaseDTO entity)
        {
            throw new NotImplementedException();
        }

        public override bool Exists(int id)
        {
            throw new NotImplementedException();
        }

        public override List<ThingBaseDTO> GetAllBase()
        {
            throw new NotImplementedException();
        }

        public override Task<List<ThingBaseDTO>> GetAllBaseAsync()
        {
            throw new NotImplementedException();
        }

        public override List<ThingFullDTO> GetAllFull()
        {
            throw new NotImplementedException();
        }

        async public override Task<List<ThingFullDTO>> GetAllFullAsync()
        {
            return await DoGetRequestFor<List<ThingFullDTO>>("Thing");
        }

        public override ThingFullDTO GetById(int id)
        {
            throw new NotImplementedException();
        }

        async public override Task<ThingFullDTO> GetByIdAsync(int id)
        {
            return await DoGetRequestFor<ThingFullDTO>($"Thing/{id}");
        }

        async public override void SaveAsync(ThingForCreationDTO entity)
        {
            var resp = await DoPostRequestFor<ThingForCreationDTO>($"Thing/Create",entity);
        }

        public override void UpdateAsync(ThingBaseDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
