using System;
using System.Threading.Tasks;
using FluentAssertions;
using ikubINFO.Api.Controllers;
using ikubINFO.Api.Logging;
using ikubINFO.Entity.Dtos;
using ikubINFO.Service.Role;
using ikubINFO.Utility.StaticData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ikubINFO.UnitTests
{
    public class RoleControllerTests
    {
        private readonly Mock<IRoleService> serviceStub = new();
        private readonly Mock<ILoggerManager> loggerStub = new();
        private readonly Random rand = new();
        [Fact]
        public async Task GetRoleAsync_WithAnExistingRole_ReturnsNotFound()
        {
            serviceStub.Setup(service => service.GetRoleAsync(It.IsAny<Guid>().ToString()))
            .ReturnsAsync((RoleDto)null);

            var controller = new RoleController(loggerStub.Object, serviceStub.Object);

            var result = await controller.GetRoleAsync(Guid.NewGuid().ToString());

            result.Result.Should().BeOfType<NotFoundResult>();
        }
        [Fact]
        public async Task GetRoleAsync_WithAnExistingRole_ReturnsExpectedRole()
        {
            var expectedRole = CreateRandomRole();
            serviceStub.Setup(service => service.GetRoleAsync(It.IsAny<string>()))
            .ReturnsAsync(expectedRole);

            var controller = new RoleController(loggerStub.Object, serviceStub.Object);

            var result = await controller.GetRoleAsync(Guid.NewGuid().ToString());

            result.Value.Should().BeEquivalentTo(expectedRole);

        }
        [Fact]
        public async Task GetRolesAsync_WithExistingRoles_ReturnsAllRoles()
        {
            var expectedRoles = new[] { CreateRandomRole(), CreateRandomRole(), CreateRandomRole() };
            serviceStub.Setup(service => service.GetRolesAsync())
            .ReturnsAsync(expectedRoles);

            var controller = new RoleController(loggerStub.Object, serviceStub.Object);

            var result = await controller.GetRolesAsync();

            result.Should().BeEquivalentTo(expectedRoles);

        }
        [Fact]
        public async Task GetRoleWithUserAsync_WithAnExistingRoleWithUser_ReturnsNotFound()
        {
            serviceStub.Setup(service => service.GetRoleWithUserAsync(It.IsAny<Guid>().ToString()))
            .ReturnsAsync((RoleWithUserDto)null);

            var controller = new RoleController(loggerStub.Object, serviceStub.Object);

            var result = await controller.GetRoleAsync(Guid.NewGuid().ToString());

            result.Result.Should().BeOfType<NotFoundResult>();
        }
        [Fact]
        public async Task GetRoleWithUserAsync_WithAnExistingRoleWithUser_ReturnsExpectedRole()
        {
            var expectedRole = CreateRandomRoleWithUser();
            serviceStub.Setup(service => service.GetRoleWithUserAsync(It.IsAny<string>()))
            .ReturnsAsync(expectedRole);

            var controller = new RoleController(loggerStub.Object, serviceStub.Object);

            var result = await controller.GetRoleWithUserAsync(Guid.NewGuid().ToString());

            result.Value.Should().BeEquivalentTo(expectedRole);

        }
        [Fact]
        public async Task GetRolesWithUserAsync_WithExistingRolesWithUser_ReturnsAllRoles()
        {
            var expectedRoles = new[] { CreateRandomRoleWithUser(), CreateRandomRoleWithUser(), CreateRandomRoleWithUser() };
            serviceStub.Setup(service => service.GetRolesWithUserAsync())
            .ReturnsAsync(expectedRoles);

            var controller = new RoleController(loggerStub.Object, serviceStub.Object);

            var result = await controller.GetRolesWithUserAsync();

            result.Should().BeEquivalentTo(expectedRoles);

        }
        [Fact]
        public async Task CreateRolesAsync_WithRoleToCreate_ReturnsStatus()
        {
            var roleToCreate = new CreateRoleDto(
               Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString()
            );
            serviceStub.Setup(service => service.AddRole(roleToCreate))
                .ReturnsAsync(new Status(StatusCodes.Status200OK, StaticData.ROLE_ADD_SUCCESS));

            var controller = new RoleController(loggerStub.Object, serviceStub.Object);

            var result = await controller.CreateRoleAsync(roleToCreate);

            result.Should().BeOfType<Status>();
            result.Should().BeEquivalentTo(new Status(StatusCodes.Status200OK, StaticData.ROLE_ADD_SUCCESS));

        }
        [Fact]
        public async Task UpdateRoleAsync_WithRoleToUpdate_ReturnsStatus()
        {
            RoleDto existingRole = CreateRandomRole();
            serviceStub.Setup(service => service.GetRoleAsync(It.IsAny<Guid>().ToString()))
               .ReturnsAsync(existingRole);
            var roleId = existingRole.RoleGuid;
            var roleToUpdate = new UpdateRoleDto(
               Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString()
            );

            serviceStub.Setup(service => service.UpdateRole(roleId, roleToUpdate))
               .ReturnsAsync(new Status(StatusCodes.Status200OK, StaticData.ROLE_UPDATE_SUCCESS));
            var controller = new RoleController(loggerStub.Object, serviceStub.Object);

            var result = await controller.UpdateRoleAsync(roleId, roleToUpdate);

            result.Should().BeOfType<Status>();
            result.Should().BeEquivalentTo(new Status(StatusCodes.Status200OK, StaticData.ROLE_UPDATE_SUCCESS));

        }
        [Fact]
        public async Task DeleteRoleAsync_WithExistingRole_ReturnsStatus()
        {
            var id = It.IsAny<Guid>().ToString();
            serviceStub.Setup(service => service.DeleteRole(id))
                .ReturnsAsync(new Status(StatusCodes.Status200OK, StaticData.ROLE_DELETE_SUCCESS));

            var controller = new RoleController(loggerStub.Object, serviceStub.Object);

            var result = await controller.DeleteRoleAsync(id);

            result.Should().BeOfType<Status>();
            result.Should().BeEquivalentTo(new Status(StatusCodes.Status200OK, StaticData.ROLE_DELETE_SUCCESS));
        }

        [Fact]
        public async Task DeleteRoleAsync_WithExistingRoleWithUser_ReturnsStatus()
        {
            var id = It.IsAny<Guid>().ToString();
            serviceStub.Setup(service => service.DeleteRoleWIthUser(id))
                .ReturnsAsync(new Status(StatusCodes.Status200OK, StaticData.ROLE_DELETE_SUCCESS));

            var controller = new RoleController(loggerStub.Object, serviceStub.Object);

            var result = await controller.DeleteRoleAsync(id,true);

            result.Should().BeOfType<Status>();
            result.Should().BeEquivalentTo(new Status(StatusCodes.Status200OK, StaticData.ROLE_DELETE_SUCCESS));
        }

        private RoleDto CreateRandomRole()
         {
            return new(
                rand.Next(1000),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString()
                );
        }

        private RoleWithUserDto CreateRandomRoleWithUser()
         {
            return new(
                rand.Next(1000),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                new[]{CreateRandomUser(),CreateRandomUser(),CreateRandomUser()}
                );
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