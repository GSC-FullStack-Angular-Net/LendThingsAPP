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
            var existingThingsDTO = Mapper.Map<IEnumerable<ThingFullDTO>>(existingThings);
            return Ok(existingThingsDTO);
        }

        [HttpGet()]
        [Route("{id}")]
        public IActionResult GetOne(int id)
        {
            var existingThing = UoW.ThingRepository.GetById(id);
            var existingThingDTO = Mapper.Map<ThingFullDTO>(existingThing);
            return Ok(existingThingDTO);
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
            
            var newThingDTO = Mapper.Map<ThingBaseDTO>(newThing);

            return Created($"api/Thing/{newThingDTO.Id}", newThingDTO);
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

            var resultDTO = Mapper.Map<ThingBaseDTO>(result);
            return Ok(resultDTO);
        }

        [HttpPatch()]
        [Route("{id}")]
        public IActionResult PartialUpdate(int id, ThingForPartialUpdateDTO thingData)
        {
            var thingExisting = UoW.ThingRepository.GetById(id);
            if (thingExisting is null)
            {
                return NoContent();
            }

            thingExisting.Description = thingData?.Description ?? thingExisting.Description;
            if(thingData.Category is not null && thingExisting.Category.Id!=thingData.Category)
            {
                var newCategory = UoW.CategoryRepository.GetById((int)thingData.Category);
                thingExisting.Category = newCategory;
            }
            UoW.CompleteAsync();

            var resultDTO = Mapper.Map<ThingBaseDTO>(thingExisting);
            return Ok(resultDTO);
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
