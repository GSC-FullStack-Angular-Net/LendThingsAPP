using LendThingsCommonClasses.DTO;
using LendThingsMVC.Configuration;
using LendThingsMVC.Models;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace LendThingsMVC.Services
{
    public class CategoryService : BaseModelService<CategoryBaseDTO,CategoryFullDTO, CategoryBaseDTO, CategoryBaseDTO, CategoryBaseDTO>, ICategoryService
    {
        public CategoryService(IOptions<APIOptions> options) : base(options)
        {
        }

        public override Task<ProcesedResponse<string>> DeleteAsync(CategoryBaseDTO entity)
        {
            throw new NotImplementedException();
        }

        public override bool Exists(int id)
        {
            throw new NotImplementedException();
        }

        async public override Task<ProcesedResponse<List<CategoryBaseDTO>>> GetAllBaseAsync()
        {
            var response = await DoGetRequestOn("Category");
            return await ProcessResponse<List<CategoryBaseDTO>>(response);
        }
        async public override Task<ProcesedResponse<CategoryFullDTO>> GetByIdAsync(int id)
        {

            var response = await DoGetRequestOn($"Category/{id}");
            return await ProcessResponse<CategoryFullDTO>(response);
        }

        public override Task<ProcesedResponse<List<CategoryFullDTO>>> GetAllFullAsync()
        {
            throw new NotImplementedException();
        }


        public override Task<ProcesedResponse<CategoryBaseDTO>> SaveAsync(CategoryBaseDTO entity)
        {
            throw new NotImplementedException();
        }

        public override Task<ProcesedResponse<CategoryBaseDTO>> UpdateAsync(CategoryBaseDTO entity)
        {
            throw new NotImplementedException();
        }
    }

}

