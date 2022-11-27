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
        async public Task<IActionResult> GetAll()
        {
            var existingThings = await UoW.ThingRepository.GetAllAsync();
            if (existingThings is null)
            {
                return NotFound();
            }
            var existingThingsDTO = Mapper.Map<IEnumerable<ThingFullDTO>>(existingThings);
            return Ok(existingThingsDTO);
        }

        [HttpGet()]
        [Route("{id}")]
        async public Task<IActionResult> GetOne(int id)
        {
            var existingThing = await UoW.ThingRepository.GetByIdAsync(id);
            if (existingThing is null)
            {
                return NotFound();
            }
            var existingThingDTO = Mapper.Map<ThingFullDTO>(existingThing);
            return Ok(existingThingDTO);
        }

        [HttpPost()]
        [Route("create")]
        async public Task<IActionResult> Create([FromBody] ThingForCreationDTO thingDTO)
        {

            if ((await UoW.ThingRepository.GetAllAsync()).FirstOrDefault(t => t.Description == thingDTO.Description) is not null)
            {
                return BadRequest($"The Thing: {thingDTO.Description} is created already.");
            }

            Thing newThing = Mapper.Map<Thing>(thingDTO);

            newThing.Category = await UoW.CategoryRepository.GetByIdAsync(thingDTO.Category);
            newThing = await UoW.ThingRepository.AddAsync(newThing);

            await UoW.CompleteAsync();
            
            var newThingDTO = Mapper.Map<ThingBaseDTO>(newThing);

            return Created($"api/Thing/{newThingDTO.Id}", newThingDTO);
        }

        [HttpPut()]
        [Route("{id}")]
        async public Task<IActionResult> Update(int id, ThingBaseDTO thingData)
        {
            var thingExisting = await UoW.ThingRepository.GetByIdAsync(id);
            if (thingExisting is null)
            {
                return NotFound();
            }
            if (thingExisting.Description != thingData.Description && (await UoW.ThingRepository.GetAllAsync()).FirstOrDefault(t => t.Description == thingData.Description) is not null)
            {
                return BadRequest($"The Thing: {thingData.Description} is created already.");
            }
            thingExisting.Description = thingData.Description;
            var newCategoy = await UoW.CategoryRepository.GetByIdAsync(thingData.Category);
            thingExisting.Category = newCategoy;

            var result = UoW.ThingRepository.Update(thingExisting);
            await UoW.CompleteAsync();

            var resultDTO = Mapper.Map<ThingBaseDTO>(result);
            return Ok(resultDTO);
        }

        [HttpPatch()]
        [Route("{id}")]
        async public Task<IActionResult> PartialUpdate(int id, ThingPartialUpdateDTO thingData)
        {
            var thingExisting = await UoW.ThingRepository.GetByIdAsync(id);
            if (thingExisting is null)
            {
                return NotFound();
            }
            if (thingExisting.Description != thingData.Description && (await UoW.ThingRepository.GetAllAsync()).FirstOrDefault(t => t.Description == thingData.Description) is not null)
            {
                return BadRequest($"The Thing: {thingData.Description} is created already.");
            }
            //Mapping Only data that has changed
            thingExisting.Description = thingData?.Description ?? thingExisting.Description;
            if(thingData!.Category is not null && thingExisting.Category.Id!=thingData.Category)
            {
                var newCategory = await UoW.CategoryRepository.GetByIdAsync((int)thingData.Category);
                thingExisting.Category = newCategory;
            }

            await UoW.CompleteAsync();

            var resultDTO = Mapper.Map<ThingBaseDTO>(thingExisting);
            return Ok(resultDTO);
        }

        [HttpDelete()]
        [Route("{id}")]
        async public Task<IActionResult> Delete(int id)
        {
            if (! await UoW.ThingRepository.DeleteAsync(id))
            {
                return NotFound();
            }
            await UoW.CompleteAsync();

            return Ok($"The Thing with id {id} has been deleted.");
        }


    }
}
