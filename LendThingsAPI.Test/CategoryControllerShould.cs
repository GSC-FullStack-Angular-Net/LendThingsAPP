using AutoMapper;
using FluentAssertions;
using LendThingsAPI.Controllers;
using LendThingsAPI.DataAccess;
using LendThingsCommonClasses.DTO;
using LendThingsCommonClasses.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Reflection.PortableExecutable;

namespace LendThingsAPI.Test
{
    public class CategoryControllerShould
    {
        private Mock<IMapper> MapperMock { get; }

        private CategoryController sut { get; }
        private Mock<IUnitOfWork> UoWMock { get; }
        private Mock<ICategoryRepository> CategoryRepositoryMock { get; }

        public CategoryControllerShould()
        {
            var expectedReturn = CategoryMockData.GetCategoriesList();
            CategoryRepositoryMock = new Mock<ICategoryRepository>();
            CategoryRepositoryMock.Setup(m => m.GetAllAsync()).Returns(Task.FromResult(expectedReturn));

            UoWMock = new Mock<IUnitOfWork>();
            UoWMock.Setup(m => m.CategoryRepository).Returns(CategoryRepositoryMock.Object);
            MapperMock = new Mock<IMapper>();

            sut = new CategoryController(UoWMock.Object, MapperMock.Object);
        }


        
        [Fact(Skip = "Esta testeando una funcion del framework, no del controller.")]
        public void Create_Returns_BadRequest_On_Request_Without_Body()
        {
            //Arrange
            var blankCategoryDTO = new CategoryForCreationDTO();

            //Act
            var result = sut.Create(blankCategoryDTO);
            //Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            result.As<BadRequestObjectResult>().Value.Should().Be("Description is mandatory");
        }


        [Fact]
        async public Task Create_Returns_BadRequest_On_Repeated_Description()
        {
            //Arrange
            var testCategoryDTO = new CategoryForCreationDTO() { Description= "Machinery" };         

            //Act
            var result = await sut.Create(testCategoryDTO);
            //Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            result.As<BadRequestObjectResult>().Value.Should().Be($"The Category: {testCategoryDTO.Description} is created already.");
        }


        [Fact]
        async public Task Create_Returns_Created_On_Successful_Request()
        {
            //Arrange
            var testCategoryDTO = new CategoryForCreationDTO() { Description = "Kitchen" };
            var expectedCategory = new Category { Id = 4, Description = testCategoryDTO.Description };
            CategoryRepositoryMock.Setup(m => m.AddAsync(It.IsAny<Category>())).Returns(Task.FromResult(expectedCategory));

            MapperMock.Setup(m => m.Map<Category>(It.IsAny<CategoryForCreationDTO>())).Returns(expectedCategory);
            //Act
            var result = await sut.Create(testCategoryDTO);
            //Assert
            result.Should().BeOfType<CreatedResult>();
            result.As<CreatedResult>().Location.Should().Be($"api/Category/{expectedCategory.Id}");
            result.As<CreatedResult>().Value.Should().Be(expectedCategory);
        }
    }
}