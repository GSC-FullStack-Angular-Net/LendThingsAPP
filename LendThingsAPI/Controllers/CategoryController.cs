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
        private IUnitOfWork uow;
        public CategoryController(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        [HttpPost()]
        [Route("create")]
        public IActionResult Create([FromBody] CategoryForCreationDTO category)
        {

            if(uow.CategoryRepository.GetAll().FirstOrDefault(c=>c.Description==category.Description) is not null)
            {
                return BadRequest($"The Category: {category.Description} is created already.");
            }
            
            Category newCategory = new Category() { Description = category.Description};
            newCategory = uow.CategoryRepository.Add(newCategory);

            uow.CompleteAsync();

            return Created("api/Category/Create", newCategory);
        }
    }
}
