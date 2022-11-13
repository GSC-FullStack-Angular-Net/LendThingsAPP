﻿using AutoMapper;
using LendThingsAPI.DataAccess;
using LendThingsAPI.DTO;
using LendThingsAPI.Models;
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
        public IActionResult GetAll()
        {
            return Ok(UoW.PersonRepository.GetAll());
        }

        [HttpGet()]
        [Route("{id}")]
        public IActionResult GetOne([FromRoute]int id)
        {
            return Ok(UoW.PersonRepository.GetById(id));
        }

        [HttpPost]
        [Route("")]
        public IActionResult Create(PersonForCreationDTO personForCreationDTO)
        {
            var personExisting = UoW.PersonRepository.GetAll().SingleOrDefault(p => p.Email == personForCreationDTO.Email);
            if (personExisting is not null)
            {
                //Esto seria un securityleak si se trataria de usuarios, pero como en este caso no es importante.
                return BadRequest($"A Person with the email {personForCreationDTO.Email} is already created. Id={personExisting.Id}.");
            }

            var newPerson = Mapper.Map<Person>(personForCreationDTO);
            UoW.PersonRepository.Add(newPerson);
            UoW.CompleteAsync();

            return Ok(newPerson);
        }

        [HttpPut()]
        [Route("{id}")]
        public IActionResult Update(int id,PersonForCreationDTO personData)
        {
            var personExisting = UoW.PersonRepository.GetById(id);
            if (personExisting is null)
            {
                return NotFound($"A Person with the id {id} is not created.");
            }

            personExisting.PhoneNumber = personData.PhoneNumber;
            personExisting.Email=personData.Email;
            personExisting.Name=personData.Name;

            var result = UoW.PersonRepository.Update(personExisting);
            UoW.CompleteAsync();

            return Ok(result);
        }

        [HttpPatch()]
        [Route("{id}")]
        public IActionResult PartialUpdate(int id, PersonForPartialUpdateDTO personData)
        {
            var personExisting = UoW.PersonRepository.GetById(id);
            if (personExisting is null)
            {
                return NotFound($"A Person with the id {id} is not created.");
            }

            personExisting.PhoneNumber = personData?.PhoneNumber ?? personExisting.PhoneNumber;
            personExisting.Email = personData?.Email ?? personExisting.Email;
            personExisting.Name = personData?.Name ?? personExisting.Name;

            UoW.CompleteAsync();

            return Ok(personExisting);
        }

        [HttpDelete()]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            if (!UoW.PersonRepository.Delete(id))
            {
                return NotFound($"A Person with the id {id} is not created.");
            }
            UoW.CompleteAsync();

            return Ok($"The Person with id {id} has been deleted.");
        }
    }
}