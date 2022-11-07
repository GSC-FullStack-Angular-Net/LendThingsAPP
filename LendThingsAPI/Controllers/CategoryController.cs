using LendThingsAPI.DataAccess;
using LendThingsAPI.DTO;
using LendThingsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Runtime.CompilerServices;

namespace LendThingsAPI.Controllers
{

    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private IUnitOfWork uow;
        public CategoryController(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        [HttpPost()]
        public IActionResult Create([FromBody] CategoryForCreationDTO category)
        {
            if (category is null
               || string.IsNullOrWhiteSpace(category.Description))
                return BadRequest("Description is mandatory");

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
