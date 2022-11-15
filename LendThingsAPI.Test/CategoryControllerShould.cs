using AutoMapper;
using FluentAssertions;
using LendThingsAPI.Controllers;
using LendThingsAPI.DataAccess;
using LendThingsAPI.DTO;
using LendThingsAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Reflection.PortableExecutable;

namespace LendThingsAPI.Test
{
    public class CategoryControllerShould
    {
        private CategoryController sut { get; }
        private Mock<IUnitOfWork> UoWMock { get; }
        private Mock<ICategoryRepository> CategoryRepositoryMock { get; }

        public CategoryControllerShould()
        {
            var expectedReturn = CategoryMockData.GetCategoriesList();
            CategoryRepositoryMock = new Mock<ICategoryRepository>();
            CategoryRepositoryMock.Setup(m => m.GetAll()).Returns(expectedReturn);

            UoWMock = new Mock<IUnitOfWork>();
            UoWMock.Setup(m => m.CategoryRepository).Returns(CategoryRepositoryMock.Object);
            var mockMapper = new Mock<IMapper>();

            sut = new CategoryController(UoWMock.Object, mockMapper.Object);
        }


        
        [Fact(Skip = "Esta testeando una funcion del framework, no del controller.")]
        public void Create_Returns_BadRequest_On_Request_Without_Body()
        {
            //Arrange
            var blankCategoryDTO = new CategoryForCreationDTO();

            //Act
            var result = (BadRequestObjectResult)sut.Create(blankCategoryDTO);
            //Assert
            Assert.Equal("Description is mandatory", result.Value);
            Assert.Equal(400, result.StatusCode);
        }


        [Fact]
        public void Create_Returns_BadRequest_On_Repeated_Description()
        {
            //Arrange
            var testCategoryDTO = new CategoryForCreationDTO() { Description= "Machinery" };         

            //Act
            var result = (BadRequestObjectResult)sut.Create(testCategoryDTO);
            //Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            result.As<BadRequestObjectResult>().Value.Should().Be($"The Category: {testCategoryDTO.Description} is created already.");
        }


        [Fact]
        public void Create_Returns_Created_On_Successful_Request()
        {
            //Arrange
            var testCategoryDTO = new CategoryForCreationDTO() { Description = "Kitchen" };
            CategoryRepositoryMock.Setup(m => m.Add(new Category() { Description = "Kitchen" })).Returns(new Category { Id = 4, Description = testCategoryDTO.Description });

            //Act
            var result = (CreatedResult)sut.Create(testCategoryDTO);
            //Assert
            Assert.Equal("api/Category/Create", result.Location);
            Assert.Equal(201, result.StatusCode);
            
        }
    }
}