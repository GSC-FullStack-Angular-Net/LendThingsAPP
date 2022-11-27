using AutoMapper;
using LendThingsAPI.DataAccess;
using LendThingsCommonClasses.DTO;
using LendThingsCommonClasses.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Runtime.CompilerServices;

namespace LendThingsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    //[Authorize(Roles = "Owner, Administrator")]
    public class CategoryController : ControllerBase
    {
        private IUnitOfWork UoW { get; }
        public IMapper Mapper { get; }
        public CategoryController(IUnitOfWork uow, IMapper mapper)
        {
            UoW = uow;
            Mapper = mapper;
        }

        [HttpGet()]
        [Route("")]
        async public Task<IActionResult> GetAll()
        {
            var existingCategories = await UoW.CategoryRepository.GetAllAsync();
            return Ok(existingCategories);
        }

        [HttpGet()]
        [Route("{id}")]
        async public Task<IActionResult> GetOne(int id)
        {
            var existingCategory = await UoW.CategoryRepository.GetByIdAsync(id);
            return Ok(existingCategory);
        }

        [HttpPost()]
        [Route("create")]
        async public Task<IActionResult> Create([FromBody] CategoryForCreationDTO category)
        {

            if((await UoW.CategoryRepository.GetAllAsync()).FirstOrDefault(c=>c.Description==category.Description) is not null)
            {
                return BadRequest($"The Category: {category.Description} is created already.");
            }

            Category newCategory = Mapper.Map<Category>(category);
            newCategory = await UoW.CategoryRepository.AddAsync(newCategory);

            await UoW.CompleteAsync();

            return Created($"api/Category/{newCategory.Id}", newCategory);
        }
    }
}
