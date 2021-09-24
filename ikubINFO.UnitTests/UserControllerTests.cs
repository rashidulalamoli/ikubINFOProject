using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using ikubINFO.Api.Controllers;
using ikubINFO.Api.Logging;
using ikubINFO.Entity.Dtos;
using ikubINFO.Service.User;
using ikubINFO.Utility.StaticData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ikubINFO.UnitTests
{
    public class UserControllerTests
    {
        private readonly Mock<IUserService> serviceStub = new();
        private readonly Mock<ILoggerManager> loggerStub = new();
        private readonly Random rand = new();
        [Fact]
        public async Task GetUserAsync_WithAnExistingUser_ReturnsNotFound()
        {
            serviceStub.Setup(service => service.GetUserAsync(It.IsAny<Guid>().ToString()))
            .ReturnsAsync((UserDto)null);

            var controller = new UserController(loggerStub.Object, serviceStub.Object);

            var result = await controller.GetUserAsync(Guid.NewGuid().ToString());

            result.Result.Should().BeOfType<NotFoundResult>();
        }
        [Fact]
        public async Task GetUserAsync_WithAnExistingUser_ReturnsExpectedUser()
        {
            var expectedUser = CreateRandomUser();
            serviceStub.Setup(service => service.GetUserAsync(It.IsAny<string>()))
            .ReturnsAsync(expectedUser);

            var controller = new UserController(loggerStub.Object, serviceStub.Object);

            var result = await controller.GetUserAsync(Guid.NewGuid().ToString());

            result.Value.Should().BeEquivalentTo(expectedUser);

        }
        [Fact]
        public async Task GetUsersAsync_WithExistingUsers_ReturnsAllUsers()
        {
            var expectedUsers = new[] { CreateRandomUser(), CreateRandomUser(), CreateRandomUser() };
            serviceStub.Setup(service => service.GetUsersAsync())
            .ReturnsAsync(expectedUsers);

            var controller = new UserController(loggerStub.Object, serviceStub.Object);

            var result = await controller.GetUsersAsync();

            result.Should().BeEquivalentTo(expectedUsers);

        }
        [Fact]
        public async Task CreateUsersAsync_WithUserToCreate_ReturnsStatus()
        {
            var userToCreate = new CreateUserDto(
               Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                rand.Next(1000)
            );
            serviceStub.Setup(service => service.AddUser(userToCreate))
                .ReturnsAsync(new Status(StatusCodes.Status200OK, StaticData.USER_ADD_SUCCESS));

            var controller = new UserController(loggerStub.Object, serviceStub.Object);

            var result = await controller.CreateUserAsync(userToCreate);

            result.Should().BeOfType<Status>();
            result.Should().BeEquivalentTo(new Status(StatusCodes.Status200OK, StaticData.USER_ADD_SUCCESS));

        }
        [Fact]
        public async Task UpdateUserAsync_WithUserToUpdate_ReturnsStatus()
        {
            UserDto existingUser = CreateRandomUser();
            serviceStub.Setup(service => service.GetUserAsync(It.IsAny<Guid>().ToString()))
               .ReturnsAsync(existingUser);
            var userId = existingUser.UserGuid;
            var userToUpdate = new UpdateUserDto(
               Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                rand.Next(1000)
            );

            serviceStub.Setup(service => service.UpdateUser(userId, userToUpdate))
               .ReturnsAsync(new Status(StatusCodes.Status200OK, StaticData.USER_UPDATE_SUCCESS));
            var controller = new UserController(loggerStub.Object, serviceStub.Object);

            var result = await controller.UpdateUserAsync(userId, userToUpdate);

            result.Should().BeOfType<Status>();
            result.Should().BeEquivalentTo(new Status(StatusCodes.Status200OK, StaticData.USER_UPDATE_SUCCESS));

        }
        [Fact]
        public async Task DeleteUserAsync_WithExistingUser_ReturnsStatus()
        {
            var id = It.IsAny<Guid>().ToString();
            serviceStub.Setup(service => service.DeleteUser(id))
                .ReturnsAsync(new Status(StatusCodes.Status200OK, StaticData.USER_DELETE_SUCCESS));

            var controller = new UserController(loggerStub.Object, serviceStub.Object);

            var result = await controller.DeleteUserAsync(id);

            result.Should().BeOfType<Status>();
            result.Should().BeEquivalentTo(new Status(StatusCodes.Status200OK, StaticData.USER_DELETE_SUCCESS));
        }

        private UserDto CreateRandomUser()
        {
            return new(
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                rand.Next(1000)
                );
        }
    }
}
