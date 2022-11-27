using AutoMapper;
using LendThingsAPI.DataAccess;
using LendThingsCommonClasses.DTO;
using LendThingsCommonClasses.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace LendThingsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Roles = "Owner, Administrator")]
    public partial class PersonController : Controller
    {

        private IUnitOfWork UoW { get; }
        public IMapper Mapper { get; }
        public PersonController(IUnitOfWork uow, IMapper mapper)
        {
            UoW = uow;
            Mapper = mapper;
        }

        [HttpGet()]
        [Route("")]
        async public Task<IActionResult> GetAll()
        {
            return Ok(await UoW.PersonRepository.GetAllAsync());
        }

        [HttpGet()]
        [Route("{id}")]
        async public Task<IActionResult> GetOne([FromRoute]int id)
        {
            var existingPerson = await UoW.PersonRepository.GetByIdAsync(id);
            if (existingPerson is null)
            {
                return NotFound();
            }
            return Ok(existingPerson);
        }

        [HttpPost]
        [Route("")]
        async public Task<IActionResult> Create(PersonForCreationDTO personForCreationDTO)
        {
            var personExisting = (await UoW.PersonRepository.GetAllAsync()).SingleOrDefault(p => p.Email == personForCreationDTO.Email);
            if (personExisting is not null)
            {
                return BadRequest(new {errors= new { Email = new string[] { $"A Person with the email {personForCreationDTO.Email} is already created. Id={personExisting.Id}." }} });
            }

            var newPerson = Mapper.Map<Person>(personForCreationDTO);
            newPerson = await UoW.PersonRepository.AddAsync(newPerson);
            await UoW.CompleteAsync();

            return Ok(newPerson);
        }

        [HttpPut()]
        [Route("{id}")]
        async public Task<IActionResult> Update(int id,PersonForCreationDTO personData)
        {
            var personExisting = await UoW.PersonRepository.GetByIdAsync(id);
            if (personExisting is null)
            {
                return NoContent();
            }
            var personWithSameEmail = (await UoW.PersonRepository.GetAllAsync()).SingleOrDefault(p => p.Email == personData.Email);
            if (personWithSameEmail is not null && personWithSameEmail.Id != id)
            {
                return BadRequest(new { errors = new { Email = new string[] { $"A Person with the email {personData.Email} is already created. Id={personWithSameEmail.Id}." } } });
            }

            personExisting.PhoneNumber = personData.PhoneNumber;
            personExisting.Email=personData.Email;
            personExisting.Name=personData.Name;

            var result = UoW.PersonRepository.Update(personExisting);
            await UoW.CompleteAsync();

            return Ok(result);
        }

        [HttpPatch()]
        [Route("{id}")]
        async public Task<IActionResult> PartialUpdate(int id, PersonForPartialUpdateDTO personData)
        {
            var personExisting = await UoW.PersonRepository.GetByIdAsync(id);
            if (personExisting is null)
            {
                return NoContent();
            }
            var personWithSameEmail = (await UoW.PersonRepository.GetAllAsync()).SingleOrDefault(p => p.Email == personData.Email);
            if (personWithSameEmail is not null && personWithSameEmail.Id != id)
            {
                return BadRequest(new { errors = new { Email = new string[] { $"A Person with the email {personData.Email} is already created. Id={personWithSameEmail.Id}." } } });
            }
            personExisting.PhoneNumber = personData?.PhoneNumber ?? personExisting.PhoneNumber;
            personExisting.Email = personData?.Email ?? personExisting.Email;
            personExisting.Name = personData?.Name ?? personExisting.Name;

            await UoW.CompleteAsync();

            return Ok(personExisting);
        }

        [HttpDelete()]
        [Route("{id}")]
        async public Task<IActionResult> Delete(int id)
        {
            if (!await UoW.PersonRepository.DeleteAsync(id))
            {
                return NoContent();
            }
            await UoW.CompleteAsync();

            return Ok(new { msg = $"The Person with id {id} has been deleted." });
        }
    }
}
