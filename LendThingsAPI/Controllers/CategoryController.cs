using AutoMapper;
using LendThingsAPI.DataAccess;
using LendThingsAPI.DTO;
using LendThingsAPI.Models;
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
        public IActionResult GetAll()
        {
            var existingCategories = UoW.CategoryRepository.GetAll();
            return Ok(existingCategories);
        }

        [HttpGet()]
        [Route("{id}")]
        public IActionResult GetOne(int id)
        {
            var existingCategory = UoW.CategoryRepository.GetById(id);
            return Ok(existingCategory);
        }

        [HttpPost()]
        [Route("create")]
        public IActionResult Create([FromBody] CategoryForCreationDTO category)
        {

            if(UoW.CategoryRepository.GetAll().FirstOrDefault(c=>c.Description==category.Description) is not null)
            {
                return BadRequest($"The Category: {category.Description} is created already.");
            }

            Category newCategory = Mapper.Map<Category>(category);
            newCategory = UoW.CategoryRepository.Add(newCategory);

            UoW.CompleteAsync();

            return Created($"api/Category/{newCategory.Id}", newCategory);
        }
    }
}
