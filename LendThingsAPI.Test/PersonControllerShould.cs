using AutoMapper;
using FluentAssertions;
using LendThingsAPI.Controllers;
using LendThingsAPI.DataAccess;
using LendThingsCommonClasses.DTO;
using LendThingsCommonClasses.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LendThingsAPI.Test
{
    public class PersonControllerShould
    {
        private PersonController sut { get; }
        public Mock<IMapper> MapperMock { get; }
        private Mock<IUnitOfWork> UoWMock { get; }
        private Mock<IPersonRepository> PersonRepositoryMock { get; }
        

        public PersonControllerShould()
        {
            
            PersonRepositoryMock = new Mock<IPersonRepository>();
            PersonRepositoryMock.Setup(m => m.GetAllAsync()).Returns(Task.FromResult(PersonMockData.GetPersonList()));

            UoWMock = new Mock<IUnitOfWork>();
            UoWMock.Setup(m => m.PersonRepository).Returns(PersonRepositoryMock.Object);

            MapperMock = new Mock<IMapper>();

            sut = new PersonController(UoWMock.Object, MapperMock.Object);
        }

        [Fact]
        async public Task GetAll_Return_Person_List()
        {
            var expectedReturn = PersonMockData.GetPersonList();

            var result = await sut.GetAll();

            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().Be(expectedReturn);
        }

        [Theory()]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        async public Task GetOne_With_Valid_Id_Return_Person(int id)
        {
            var expectedResul = PersonMockData.GetPersonList().First(p => p.Id == id);
            PersonRepositoryMock.Setup(m => m.GetByIdAsync(id)).Returns(Task.FromResult(expectedResul));

            var result = await sut.GetOne(id);

            result.Should().BeOfType<OkObjectResult>();
            var a = result.As<OkObjectResult>().Value;
            result.As<OkObjectResult>().Value.Should().Be(expectedResul);
        }

        [Fact]
        async public Task GetOne_With_Invalid_Id_Return_NotFound()
        {

            var resul = await sut.GetOne(4);

            resul.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        async public Task Create_With_Created_Email_Return_BadRequest()
        {
            var firstPersonOfList = PersonMockData.GetPersonList().First();
            var personDTOToTest = new PersonForCreationDTO() { Name = "", PhoneNumber = "", Email = firstPersonOfList.Email };

            var result = await sut.Create(personDTOToTest);

            result.Should().BeOfType<BadRequestObjectResult>();
            result.As<BadRequestObjectResult>().Value.Should().BeEquivalentTo(new { errors = new { Email = new string[] { $"A Person with the email {firstPersonOfList.Email} is already created. Id={firstPersonOfList.Id}." } } });
        }

        [Fact]
        async public Task Create_Return_Ok_With_New_Person()
        {
            var personDTOToTest = new PersonForCreationDTO() 
            {
                    Name = "Martin", PhoneNumber = "111111111", Email = "z@z.com" 
            };
            var newPersonWithoutId = new Person()
            {
                Name = "Martin", PhoneNumber = "111111111", Email = "z@z.com"
            };
            var newPersonWithId = new Person()
            {
                Id = 4,
                Name = "Martin",
                PhoneNumber = "111111111",
                Email = "z@z.com"
            };
            MapperMock.Setup(m=>m.Map<Person>(personDTOToTest)).Returns(newPersonWithoutId);
            PersonRepositoryMock.Setup(m => m.AddAsync(newPersonWithoutId)).Returns(Task.FromResult(newPersonWithId));

            var result = await sut.Create(personDTOToTest);

            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().BeEquivalentTo(newPersonWithId);
        }

        [Fact]
        async public Task Update_With_Unexisting_Person_Return_NoContent()
        {
            int idOnTest = 5;
            var person = new PersonForCreationDTO();
            PersonRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(null as Person));

            var result = await sut.Update(idOnTest, person);

            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        async public Task Update_Return_Ok_With_Updated_Person() 
        {
            var personBeforeUpdate= new Person() { Id = 99};
            var personAfterUpdate = new Person() { Id=99,Email = "z@z.com", Name = "Martin" , PhoneNumber = "111111111" };
            var person = new PersonForCreationDTO() { Email = personAfterUpdate.Email, Name = personAfterUpdate.Name,PhoneNumber= personAfterUpdate.PhoneNumber };
            
            PersonRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(personBeforeUpdate));
            PersonRepositoryMock.Setup(m => m.Update(It.IsAny<Person>())).Returns(personAfterUpdate);

            var result = await sut.Update(personBeforeUpdate.Id, person);

            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().BeEquivalentTo(personAfterUpdate);
        }

        [Fact]
        async public Task PartialUpdate_Ok_With_Updated_Person()
        {
            var personBeforeUpdate = new Person() { Id = 99, Email = "z@z.com", Name = "Martin", PhoneNumber = "111111111" };
            var personAfterUpdate = new Person() { Id = 99, Email = "z@z.com", Name = "Update", PhoneNumber = "222222222" };
            var person = new PersonForPartialUpdateDTO() { Name = personAfterUpdate.Name, PhoneNumber = personAfterUpdate.PhoneNumber };

            PersonRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(personBeforeUpdate));

            var result = await sut.PartialUpdate(personBeforeUpdate.Id, person);

            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().BeEquivalentTo(personAfterUpdate);
        }

        [Fact]
        async public Task PartialUpdate_With_Unexisting_Person_Return_NoContent()
        {
            int idOnTest = 5;
            var person = new PersonForPartialUpdateDTO();
            PersonRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(null as Person));

            var result = await sut.PartialUpdate(idOnTest, person);

            result.Should().BeOfType<NoContentResult>();
        }

        [Theory]
        [InlineData(5)]
        [InlineData(33)]
        [InlineData(90)]
        async public Task Delete_Return_NoContent_On_Unexisting_Id(int testId)
        {
            PersonRepositoryMock.Setup(m=>m.DeleteAsync(testId)).Returns(Task.FromResult(false));

            var result = await sut.Delete(testId);

            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        async public Task Delete_Return_Ok_With_Message()
        {
            int testId = 5;
            PersonRepositoryMock.Setup(m => m.DeleteAsync(testId)).Returns(Task.FromResult(true));

            var result = await sut.Delete(testId);

            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().BeEquivalentTo(new {msg= $"The Person with id {testId} has been deleted." });

        }

    }
}