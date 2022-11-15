using AutoMapper;
using FluentAssertions;
using LendThingsAPI.Configuration;
using LendThingsAPI.Controllers;
using LendThingsAPI.DataAccess;
using LendThingsAPI.DTO;
using LendThingsAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LendThingsAPI.Test
{
    public class LoginControllerShould
    {

        private LoginController sut { get; }
        //Como hacer el Mock de UserManager (Lo que encontre es haciendo una interfaz que la simule a mano)
        private Mock<UserManager<User>> MockUserManager { get; }
        private Mock<IOptions<JwtOptions>> MockOptionsJwt { get; }

        public LoginControllerShould()
        {
            MockUserManager = new Mock<UserManager<User>>();
            
            MockOptionsJwt = new Mock<IOptions<JwtOptions>>();

            sut = new LoginController(MockUserManager.Object, MockOptionsJwt.Object);
        }

        [Fact]
        async public void Return_Forbid_On_Unregistered_User()
        {
            //Arrange
            var testingUserForLoginDTO = new UserForLoginDTO() { Password = "", UserName = "Luna" };
            MockUserManager.Setup(m => m.FindByNameAsync(testingUserForLoginDTO.UserName)).Returns(null as Task<User>);
            //Act
            var res = await sut.Login(testingUserForLoginDTO);

            //Assert
            res.Should().BeOfType<ForbidResult>();
        }

    }
}
