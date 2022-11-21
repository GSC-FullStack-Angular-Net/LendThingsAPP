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

        async public override Task<CategoryFullDTO> GetByIdAsync(int id)
        {
            return await DoGetRequestFor<CategoryFullDTO>($"Category/{id}");
        }


        public override bool Exists(int id)
        {
            throw new NotImplementedException();
        }


        async public override Task<List<CategoryBaseDTO>> GetAllBaseAsync()
        {
            return await DoGetRequestFor<List<CategoryBaseDTO>>($"Category");
        }


        public override Task<List<CategoryFullDTO>> GetAllFullAsync()
        {
            throw new NotImplementedException();
        }


        public override Task UpdateAsync(CategoryBaseDTO entity)
        {
            throw new NotImplementedException();
        }

        
        public override Task DeleteAsync(CategoryBaseDTO entity)
        {
            throw new NotImplementedException();
        }

        public override Task SaveAsync(CategoryBaseDTO entity)
        {
            throw new NotImplementedException();
        }

    }
}
