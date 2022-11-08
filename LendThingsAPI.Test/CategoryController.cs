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
        private readonly CategoryController _categoryController;
        private readonly Mock<UnitOfWork> uowMock;

        public CategoryControllerShould()
        {
        }

        //Esto lo hace automaticamente el Framework
        //[Fact]
        //public void Create_Returns_BadRequest_On_Request_Without_Body()
        //{
        //    //Arrange
        //    var blankCategoryDTO = new CategoryForCreationDTO();

        //    var expectedReturn = new List<Category>() {
        //        new Category { Id = 1, Description = "Machinery" }, 
        //        new Category { Id = 2, Description = "School" }, 
        //        new Category { Id = 3, Description = "Computer" } 
        //    };
        //    var mockCategoryRepo = new Mock<ICategoryRepository>();
        //    mockCategoryRepo.Setup(m => m.GetAll()).Returns(expectedReturn);

        //    var mockUnitOfWork = new Mock<IUnitOfWork>();
        //    mockUnitOfWork.Setup(m => m.CategoryRepository).Returns(mockCategoryRepo.Object);

        //    var sut = new CategoryController(mockUnitOfWork.Object);

        //    //Act
        //    var result = (BadRequestObjectResult)sut.Create(blankCategoryDTO);
        //    //Assert
        //    Assert.Equal("Description is mandatory",result.Value);
        //    Assert.Equal(400, result.StatusCode);
        //}


        [Fact]
        public void Create_Returns_BadRequest_On_Repeated_Description()
        {
            //Arrange
            var testCategoryDTO = new CategoryForCreationDTO() { Description= "Machinery" };

            var expectedReturn = new List<Category>() {
                new Category { Id = 1, Description = "Machinery" },
                new Category { Id = 2, Description = "School" },
                new Category { Id = 3, Description = "Computer" }
            };
            var mockCategoryRepo = new Mock<ICategoryRepository>();
            mockCategoryRepo.Setup(m => m.GetAll()).Returns(expectedReturn);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.CategoryRepository).Returns(mockCategoryRepo.Object);

            var sut = new CategoryController(mockUnitOfWork.Object);

            //Act
            var result = (BadRequestObjectResult)sut.Create(testCategoryDTO);
            //Assert
            Assert.Equal($"The Category: {testCategoryDTO.Description} is created already.", result.Value);
            Assert.Equal(400, result.StatusCode);
        }




        [Fact]
        public void Create_Returns_Created_On_Successful_Request()
        {
            //Arrange
            var testCategoryDTO = new CategoryForCreationDTO() { Description = "Kitchen" };

            var expectedReturn = new List<Category>() {
                new Category { Id = 1, Description = "Machinery" },
                new Category { Id = 2, Description = "School" },
                new Category { Id = 3, Description = "Computer" }
            };
            var mockCategoryRepo = new Mock<ICategoryRepository>();
            mockCategoryRepo.Setup(m => m.GetAll()).Returns(expectedReturn);
            mockCategoryRepo.Setup(m => m.Add(new Category() { Description = "Kitchen" })).Returns(new Category { Id = 4, Description = testCategoryDTO.Description });

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.CategoryRepository).Returns(mockCategoryRepo.Object);

            var sut = new CategoryController(mockUnitOfWork.Object);

            //Act
            var result = (CreatedResult)sut.Create(testCategoryDTO);
            //Assert
            Assert.Equal("api/Category/Create", result.Location);
            Assert.Equal(201, result.StatusCode);
            
        }
    }
}