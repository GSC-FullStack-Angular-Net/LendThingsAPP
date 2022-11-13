using AutoMapper;
using LendThingsAPI.Controllers;
using LendThingsAPI.DataAccess;
using LendThingsAPI.Models;
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
        private Mock<IUnitOfWork> UoWMock { get; }
        private Mock<IPersonRepository> PersonRepositoryMock { get; }

        public PersonControllerShould()
        {
            var expectedReturn = new List<Person>() {
                new Person { Id = 1, Name="Fist",Email="a@a.com", PhoneNumber="111111111" },
                new Person { Id = 2, Name="Second",Email="b@b.com", PhoneNumber="222222222"},
                new Person { Id = 3, Name="Third",Email="c@c.com", PhoneNumber="333333333"}
            };
            PersonRepositoryMock = new Mock<IPersonRepository>();
            PersonRepositoryMock.Setup(m => m.GetAll()).Returns(expectedReturn);

            UoWMock = new Mock<IUnitOfWork>();
            UoWMock.Setup(m => m.PersonRepository).Returns(PersonRepositoryMock.Object);
            var mockMapper = new Mock<IMapper>();

            sut = new PersonController(UoWMock.Object, mockMapper.Object);
        }







    }
}