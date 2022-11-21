using LendThingsCommonClasses.DTO;
using LendThingsMVC.Configuration;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

namespace LendThingsMVC.Services
{
    public class CategoryService : BaseModelService<CategoryBaseDTO,CategoryFullDTO, CategoryBaseDTO, CategoryBaseDTO, CategoryBaseDTO>, ICategoryService
    {
        public CategoryService(IOptions<APIOptions> options) : base(options)
        {
        }

        public override void Delete(CategoryBaseDTO entity)
        {
            throw new NotImplementedException();
        }

        public override void DeleteAsync(CategoryBaseDTO entity)
        {
            throw new NotImplementedException();
        }

        public override bool Exists(int id)
        {
            throw new NotImplementedException();
        }

        public override List<CategoryBaseDTO> GetAllBase()
        {
            throw new NotImplementedException();
        }

        async public override Task<List<CategoryBaseDTO>> GetAllBaseAsync()
        {
            return await DoGetRequestFor<List<CategoryBaseDTO>>($"Category");
        }

        public override List<CategoryFullDTO> GetAllFull()
        {
            throw new NotImplementedException();
        }

        public override Task<List<CategoryFullDTO>> GetAllFullAsync()
        {
            throw new NotImplementedException();
        }

        public override CategoryFullDTO GetById(int id)
        {
            throw new NotImplementedException();
        }

        async public override Task<CategoryFullDTO> GetByIdAsync(int id)
        {
            return await DoGetRequestFor<CategoryFullDTO>($"Category/{id}");
        }

        

        public override void SaveAsync(CategoryBaseDTO entity)
        {
            throw new NotImplementedException();
        }


        public override void UpdateAsync(CategoryBaseDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
