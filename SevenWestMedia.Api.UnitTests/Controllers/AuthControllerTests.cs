using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using SevenWestMedia.Api.Demo.Controllers;
using SevenWestMedia.Api.Demo.Services;
using Xunit;

namespace SevenWestMedia.Api.UnitTests.Controllers
{
    public class AuthControllerTests
    {
        private readonly AuthController _target;
        private readonly IAuthService _authService;

        public AuthControllerTests()
        {
            _authService = Substitute.For<IAuthService>();
            _target = new AuthController(_authService);
        }

        [Fact]
        public async void GetAuthToken_should_return_valid_response()
        {
            // Arrange
            _authService.GetAuthToken("key").Returns("token");

            // Act
            var response = await _target.Get("key");

            // Assert
            await _authService.Received(1).GetAuthToken("key");
            response.Result.Should().BeOfType<OkObjectResult>();
            var result = ((ObjectResult)response.Result).Value;
            result.Should().Be("token");
        }
    }
}
