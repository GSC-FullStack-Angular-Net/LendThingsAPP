using AutoMapper;
using FluentAssertions;
using LendThingsAPI.Controllers;
using LendThingsAPI.DataAccess;
using LendThingsAPI.DTO;
using LendThingsAPI.Models;
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
            PersonRepositoryMock.Setup(m => m.GetAll()).Returns(PersonMockData.GetPersonList());

            UoWMock = new Mock<IUnitOfWork>();
            UoWMock.Setup(m => m.PersonRepository).Returns(PersonRepositoryMock.Object);

            MapperMock = new Mock<IMapper>();

            sut = new PersonController(UoWMock.Object, MapperMock.Object);
        }

        [Fact]
        public void GetAll_Return_Person_List()
        {
            var expectedReturn = PersonMockData.GetPersonList();

            var result = sut.GetAll();

            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().Be(expectedReturn);
        }

        [Theory()]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void GetOne_With_Valid_Id_Return_Person(int id)
        {
            var expectedResul = PersonMockData.GetPersonList().First(p => p.Id == id);
            PersonRepositoryMock.Setup(m => m.GetById(id)).Returns(expectedResul);

            var result = sut.GetOne(id);

            result.Should().BeOfType<OkObjectResult>();
            var a = result.As<OkObjectResult>().Value;
            result.As<OkObjectResult>().Value.Should().Be(expectedResul);
        }

        [Fact]
        public void GetOne_With_Invalid_Id_Return_NoContent()
        {

            var resul = sut.GetOne(4);

            resul.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public void Create_With_Created_Email_Return_BadRequest()
        {
            var firstPersonOfList = PersonMockData.GetPersonList().First();
            var personDTOToTest = new PersonForCreationDTO() { Name = "", PhoneNumber = "", Email = firstPersonOfList.Email };

            var result = sut.Create(personDTOToTest);

            result.Should().BeOfType<BadRequestObjectResult>();
            result.As<BadRequestObjectResult>().Value.Should().Be($"A Person with the email {firstPersonOfList.Email} is already created. Id={firstPersonOfList.Id}.");
        }

        [Fact]
        public void Create_Return_Ok_With_New_Person()
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
            PersonRepositoryMock.Setup(m => m.Add(newPersonWithoutId)).Returns(newPersonWithId);

            var result = sut.Create(personDTOToTest);

            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().BeEquivalentTo(newPersonWithId);
        }

        [Fact]
        public void Update_With_Unexisting_Person_Return_NoContent()
        {
            int idOnTest = 5;
            var person = new PersonForCreationDTO();
            PersonRepositoryMock.Setup(m => m.GetById(It.IsAny<int>())).Returns(null as Person);

            var result = sut.Update(idOnTest, person);

            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public void Update_Return_Ok_With_Updated_Person() 
        {
            var personBeforeUpdate= new Person() { Id = 99};
            var personAfterUpdate = new Person() { Id=99,Email = "z@z.com", Name = "Martin" , PhoneNumber = "111111111" };
            var person = new PersonForCreationDTO() { Email = personAfterUpdate.Email, Name = personAfterUpdate.Name,PhoneNumber= personAfterUpdate.PhoneNumber };
            
            PersonRepositoryMock.Setup(m => m.GetById(It.IsAny<int>())).Returns(personBeforeUpdate);
            PersonRepositoryMock.Setup(m => m.Update(It.IsAny<Person>())).Returns(personAfterUpdate);

            var result = sut.Update(personBeforeUpdate.Id, person);

            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().BeEquivalentTo(personAfterUpdate);
        }

        [Fact]
        public void PartialUpdate_Ok_With_Updated_Person()
        {
            var personBeforeUpdate = new Person() { Id = 99, Email = "z@z.com", Name = "Martin", PhoneNumber = "111111111" };
            var personAfterUpdate = new Person() { Id = 99, Email = "z@z.com", Name = "Update", PhoneNumber = "222222222" };
            var person = new PersonForPartialUpdateDTO() { Name = personAfterUpdate.Name, PhoneNumber = personAfterUpdate.PhoneNumber };

            PersonRepositoryMock.Setup(m => m.GetById(It.IsAny<int>())).Returns(personBeforeUpdate);

            var result = sut.PartialUpdate(personBeforeUpdate.Id, person);

            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().BeEquivalentTo(personAfterUpdate);
        }

        [Fact]
        public void PartialUpdate_With_Unexisting_Person_Return_NoContent()
        {
            int idOnTest = 5;
            var person = new PersonForPartialUpdateDTO();
            PersonRepositoryMock.Setup(m => m.GetById(It.IsAny<int>())).Returns(null as Person);

            var result = sut.PartialUpdate(idOnTest, person);

            result.Should().BeOfType<NoContentResult>();
        }

        [Theory]
        [InlineData(5)]
        [InlineData(33)]
        [InlineData(90)]
        public void Delete_Return_NoContent_On_Unexisting_Id(int testId)
        {
            PersonRepositoryMock.Setup(m=>m.Delete(testId)).Returns(false);

            var result = sut.Delete(testId);

            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public void Delete_Return_Ok_With_Message()
        {
            int testId = 5;
            PersonRepositoryMock.Setup(m => m.Delete(testId)).Returns(true);

            var result = sut.Delete(testId);

            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().Be($"The Person with id {testId} has been deleted.");

        }

    }
}