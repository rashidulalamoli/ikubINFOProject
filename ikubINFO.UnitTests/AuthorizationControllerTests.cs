using System;
using System.Threading.Tasks;
using FluentAssertions;
using ikubINFO.Api.Controllers;
using ikubINFO.Api.Logging;
using ikubINFO.Entity.Dtos;
using ikubINFO.Service.Authorization;
using ikubINFO.Utility.StaticData;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace ikubINFO.UnitTests
{
    public class AuthorizationControllerTests
    {
        private readonly Mock<ILoggerManager> loggerStub = new();
        private readonly Mock<IAuthorizationService> serviceStub = new();
        private readonly Random rand = new();
        [Fact]
        public async Task Token_WithValidGrantType_ReturnsTokenResult()
        {
            string[] names = new[] { "password", "refresh_token" };
            int index = rand.Next(names.Length);
            var tokenRequest = new JwtTokenInfo(
                names[index],
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString()
            );

            serviceStub.Setup(service => service.Token(tokenRequest))
                .ReturnsAsync(new TokenResult(Guid.NewGuid().ToString(), null,null, StatusCodes.Status200OK,StaticData.PASSWORD_MATCHED));

            var controller = new AuthorizationController(loggerStub.Object, serviceStub.Object);
            
            var result = await controller.Token(tokenRequest);

            result.Should().BeOfType<TokenResult>();
        }
    }
}