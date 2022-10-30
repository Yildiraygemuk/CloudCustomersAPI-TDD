using CloudCustomers.Test.Fixtures;
using CloudCustomersAPI.Controllers;
using CloudCustomersAPI.Model;
using CloudCustomersAPI.Services.Abstract;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CloudCustomers.Test
{
    public class UserControllerTest
    {
        [Fact]
        public async Task Get_OnSuccess_ReturnsStatusCode200()
        {
            //Arrange
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(
                service => service.GetAllUsers())
                 .ReturnsAsync(UsersFixture.GetTestUsers());
            var sut = new UserController(mockUserService.Object);

            //Act
            var result = (OkObjectResult)await sut.Get();

            //Assert
            result.StatusCode.Should().Be(200);

        }

        [Fact]
        public async Task Get_OnSuccess_InvokesUserService()
        {
            //Arrange
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(
                service => service.GetAllUsers())
                .ReturnsAsync(new List<User>());
            var sut = new UserController(mockUserService.Object);
            //Act
            var result = await sut.Get();

            //Assert
            mockUserService.Verify(
                service => service.GetAllUsers(),
                Times.Once());


        }
        [Fact]
        public async Task Get_OnSuccess_ReturnsListOfUsers()
        {
            //Arrange
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(
                service => service.GetAllUsers())
                .ReturnsAsync(UsersFixture.GetTestUsers());
            var sut = new UserController(mockUserService.Object);
            //Act
            var result = await sut.Get();

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            var objectResult = (OkObjectResult)result;
            objectResult.Value.Should().BeOfType<List<User>>();


        }
        [Fact]
        public async Task Get_OnNoUsersFound_Returns404()
        {
            //Arrange
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(
                service => service.GetAllUsers())
                .ReturnsAsync(new List<User>());
            var sut = new UserController(mockUserService.Object);
            //Act
            var result = await sut.Get();

            //Assert
            result.Should().BeOfType<NotFoundResult>();
            var objectResult = (NotFoundResult)result;
            objectResult.StatusCode.Should().Be(404);

        }
    }
}