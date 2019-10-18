using System.Collections.Generic;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using SevenWestMedia.Api.Demo.Controllers;
using SevenWestMedia.Api.Demo.Services;
using Xunit;

namespace SevenWestMedia.Api.UnitTests.Controllers
{
    public class UserControllerTests
    {
        private readonly UserController _target;
        private readonly IUserService _userService;

        public UserControllerTests()
        {
            _userService = Substitute.For<IUserService>();
            _target = new UserController(_userService);
        }

        [Fact]
        public void GetUserNameById_should_return_valid_response()
        {
            // Arrange
            _userService.GetUserNameById(23).Returns("Ravi Sontale");

            // Act
            var response = _target.GetUserNameById(23);

            // Assert
            _userService.Received(1).GetUserNameById(23);
            response.Result.Should().BeOfType<OkObjectResult>();
            var result = ((ObjectResult)response.Result).Value;
            result.Should().Be("Ravi Sontale");
        }

        [Fact]
        public void GetUsersByAge_should_return_valid_response()
        {
            // Arrange
            _userService.GetUsersByAge(23).Returns("Ravi,Tim");

            // Act
            var response = _target.GetUsersByAge(23);

            // Assert
            _userService.Received(1).GetUsersByAge(23);
            response.Result.Should().BeOfType<OkObjectResult>();
            var result = ((ObjectResult)response.Result).Value;
            result.Should().Be("Ravi,Tim");
        }

        [Fact]
        public void GetByGendersPerAge_should_return_valid_response()
        {
            // Arrange
            _userService.GetByGendersPerAge().Returns(new List<string>
            {
                "Age : 35 M: 1 F: 1", "Age : 40 F: 1"
            });

            // Act
            var response = _target.GetByGendersPerAge();

            // Assert
            _userService.Received(1).GetByGendersPerAge();
            response.Result.Should().BeOfType<OkObjectResult>();
            var result = (List<string>)((ObjectResult)response.Result).Value;
            result.Count.Should().Be(2);
            result[0].Should().Be("Age : 35 M: 1 F: 1");
            result[1].Should().Be("Age : 40 F: 1");
        }
    }
}
