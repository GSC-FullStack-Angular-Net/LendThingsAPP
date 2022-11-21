using AutoMapper;
using LendThingsAPI.DataAccess;
using LendThingsCommonClasses.DTO;
using LendThingsCommonClasses.Models;
using Microsoft.AspNetCore.Mvc;

namespace LendThingsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    //[Authorize(Roles = "Owner, Administrator")]
    public class ThingController : Controller
    {
        private IUnitOfWork UoW { get; }
        public IMapper Mapper { get; }
        public ThingController(IUnitOfWork uow, IMapper mapper)
        {
            UoW = uow;
            Mapper = mapper;
        }


        [HttpGet()]
        [Route("")]
        public IActionResult GetAll()
        {
            var existingThings = UoW.ThingRepository.GetAll();
            return Ok(existingThings);
        }

        [HttpGet()]
        [Route("{id}")]
        public IActionResult GetOne(int id)
        {
            var existingThing = UoW.ThingRepository.GetById(id);
            return Ok(existingThing);
        }

        [HttpPost()]
        [Route("create")]
        public IActionResult Create([FromBody] ThingForCreationDTO thingDTO)
        {

            if (UoW.ThingRepository.GetAll().FirstOrDefault(t => t.Description == thingDTO.Description) is not null)
            {
                return BadRequest($"The Thing: {thingDTO.Description} is created already.");
            }

            Thing newThing = Mapper.Map<Thing>(thingDTO);

            newThing.Category = UoW.CategoryRepository.GetById(thingDTO.Category);
            newThing = UoW.ThingRepository.Add(newThing);

            UoW.CompleteAsync();

            return Created($"api/Thing/{newThing.Id}", newThing);
        }

        [HttpPut()]
        [Route("{id}")]
        public IActionResult Update(int id, ThingBaseDTO thingData)
        {
            var thingExisting = UoW.ThingRepository.GetById(id);
            if (thingExisting is null)
            {
                return NoContent();
            }

            thingExisting.Description = thingData.Description;
            var newCategoy = UoW.CategoryRepository.GetById(thingData.Category);
            thingExisting.Category = newCategoy;

            var result = UoW.ThingRepository.Update(thingExisting);
            UoW.CompleteAsync();

            return Ok(result);
        }

        [HttpPatch()]
        [Route("{id}")]
        public IActionResult PartialUpdate(int id, ThingForPartialUpdateDTO thingData)
        {
            var personExisting = UoW.ThingRepository.GetById(id);
            if (personExisting is null)
            {
                return NoContent();
            }

            personExisting.Description = thingData?.Description ?? personExisting.Description;
            if(thingData.Category is not null && personExisting.Category.Id!=thingData.Category)
            {
                var newCategory = UoW.CategoryRepository.GetById((int)thingData.Category);
                personExisting.Category = newCategory;
            }
            UoW.CompleteAsync();

            return Ok(personExisting);
        }

        [HttpDelete()]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            if (!UoW.ThingRepository.Delete(id))
            {
                return NoContent();
            }
            UoW.CompleteAsync();

            return Ok($"The Thing with id {id} has been deleted.");
        }


    }
}
