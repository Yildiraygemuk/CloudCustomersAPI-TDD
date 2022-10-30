using CloudCustomers.Test.Fixtures;
using CloudCustomers.Test.Helper;
using CloudCustomersAPI.Config;
using CloudCustomersAPI.Model;
using CloudCustomersAPI.Services.Concrete;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudCustomers.Test.Systems.Services
{
    public class TestUserService
    {
        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokesHttpGetRequest()
        {
            //Arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);
            var endpoint = "https://example.com";
            var config = Options.Create(
                new UsersApiOptions
                {
                    Endpoint = endpoint
                });
            var sut = new UserService(httpClient, config);

            //Act
            await sut.GetAllUsers();

            //Assert
            //Verify Http request is made!
            handlerMock
                .Protected()
                .Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>()
                );
        }
        [Fact]
        public async Task GetAllUsers_WhenHits404_ReturnsEmptyListOfUsers()
        {
            //Arrange
            var handlerMock = MockHttpMessageHandler<User>.SetupReturn404();
            var httpClient = new HttpClient(handlerMock.Object);
            var endpoint = "https://example.com";
            var config = Options.Create(
                new UsersApiOptions
                {
                    Endpoint = endpoint
                });
            var sut = new UserService(httpClient, config);

            //Act
            var result = await sut.GetAllUsers();


            //Assert
            result.Count.Should().Be(0);
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_ReturnsListOfUsersOfExpectedSize()
        {
            //Arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>
                .SetupBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);
            var endpoint = "https://example.com";
            var config = Options.Create(
                new UsersApiOptions
                {
                    Endpoint = endpoint
                });
            var sut = new UserService(httpClient, config);

            //Act
            var result = await sut.GetAllUsers();


            //Assert
            result.Count.Should().Be(expectedResponse.Count);
        }
        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokesConfiguredExternalUrl()
        {
            //Arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var endpoint = "https://example.com/users";
            var handlerMock = MockHttpMessageHandler<User>
                .SetupBasicGetResourceList(expectedResponse,endpoint);
            var httpClient = new HttpClient(handlerMock.Object);
            var config = Options.Create(new UsersApiOptions
            {
                Endpoint = endpoint,
            });
            var sut = new UserService(httpClient,config);

            //Act
            var result = await sut.GetAllUsers();


            //Assert
            handlerMock
               .Protected()
               .Verify(
               "SendAsync",
               Times.Exactly(1),
               ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get && req.RequestUri.ToString() == endpoint),
               ItExpr.IsAny<CancellationToken>()
               );
        }
    }
}
