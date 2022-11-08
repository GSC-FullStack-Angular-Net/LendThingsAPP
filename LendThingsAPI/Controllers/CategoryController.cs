using AutoMapper;
using LendThingsAPI.DataAccess;
using LendThingsAPI.DTO;
using LendThingsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Runtime.CompilerServices;

namespace LendThingsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private IUnitOfWork uow { get; }
        public IMapper Mapper { get; }
        public CategoryController(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            Mapper = mapper;
        }


        [HttpPost()]
        [Route("create")]
        public IActionResult Create([FromBody] CategoryForCreationDTO category)
        {

            if(uow.CategoryRepository.GetAll().FirstOrDefault(c=>c.Description==category.Description) is not null)
            {
                return BadRequest($"The Category: {category.Description} is created already.");
            }

            Category newCategory = Mapper.Map<Category>(category);
            newCategory = uow.CategoryRepository.Add(newCategory);

            uow.CompleteAsync();

            return Created("api/Category/Create", newCategory);
        }
    }
}
