using System.Collections.Generic;
using FluentAssertions;
using NSubstitute;
using SevenWestMedia.Api.Demo.Models;
using SevenWestMedia.Api.Demo.Providers;
using SevenWestMedia.Api.Demo.Services;
using Xunit;

namespace SevenWestMedia.Api.UnitTests.Services
{
    public class UserServiceTests
    {
        private readonly UserService _target;
        private readonly IDataProvider<User> _dataProvider;

        public UserServiceTests()
        {
            _dataProvider = Substitute.For<IDataProvider<User>>();
            _target = new UserService(_dataProvider);
        }

        [Fact]
        public void GetUserNameById_should_return_valid_result()
        {
            // Arrange
            _dataProvider.Provide().Returns(new List<User>
            {
                new User
                {
                    Age = 35,
                    First = "Ravi",
                    Gender = 'M',
                    Id = 12,
                    Last = "Sontale"
                }
            });

            // Act
            var result = _target.GetUserNameById(12);

            // Assert
            result.Should().NotBeNullOrWhiteSpace();
            result.Should().Be("Ravi Sontale");
            _dataProvider.Received(1).Provide();
        }

        [Fact]
        public void GetUserNameById_should_not_return_empty_when_user_record_does_not_exist()
        {
            // Arrange
            _dataProvider.Provide().Returns(new List<User>
            {
                new User
                {
                    Age = 35,
                    First = "Ravi",
                    Gender = 'M',
                    Id = 12,
                    Last = "Sontale"
                }
            });

            // Act
            var result = _target.GetUserNameById(13);

            // Assert
            result.Should().BeNullOrWhiteSpace();
            _dataProvider.Received(1).Provide();
        }

        [Fact]
        public void GetUsersByAge_should_return_valid_result()
        {
            // Arrange
            _dataProvider.Provide().Returns(new List<User>
            {
                new User
                {
                    Age = 35,
                    First = "Ravi",
                    Gender = 'M',
                    Id = 12,
                    Last = "Sontale"
                },
                new User
                {
                    Age = 35,
                    First = "Tim",
                    Gender = 'M',
                    Id = 13,
                    Last = "Kerr"
                }
            });

            // Act
            var result = _target.GetUsersByAge(35);

            // Assert
            result.Should().NotBeNullOrWhiteSpace();
            result.Should().Be("Ravi,Tim");
            _dataProvider.Received(1).Provide();
        }

        [Fact]
        public void GetUsersByAge_should_not_return_empty_when_user_records_does_not_match()
        {
            // Arrange
            _dataProvider.Provide().Returns(new List<User>
            {
                new User
                {
                    Age = 35,
                    First = "Ravi",
                    Gender = 'M',
                    Id = 12,
                    Last = "Sontale"
                },
                new User
                {
                    Age = 35,
                    First = "Tim",
                    Gender = 'M',
                    Id = 13,
                    Last = "Kerr"
                }
            });

            // Act
            var result = _target.GetUsersByAge(36);

            // Assert
            result.Should().BeNullOrWhiteSpace();
            _dataProvider.Received(1).Provide();
        }

        [Fact]
        public void GetByGendersPerAge_should_return_valid_result()
        {
            // Arrange
            _dataProvider.Provide().Returns(new List<User>
            {
                new User
                {
                    Age = 35,
                    First = "Ravi",
                    Gender = 'M',
                    Id = 12,
                    Last = "Sontale"
                },
                new User
                {
                    Age = 35,
                    First = "Shyamal",
                    Gender = 'F',
                    Id = 13,
                    Last = "Raje"
                },
                new User
                {
                    Age = 40,
                    First = "Tim",
                    Gender = 'F',
                    Id = 14,
                    Last = "Kerr"
                }
            });

            // Act
            var result = _target.GetByGendersPerAge();

            // Act
            result.Count.Should().Be(2);
            result[0].Should().Be("Age : 35 M: 1 F: 1");
            result[1].Should().Be("Age : 40 F: 1");
        }
    }
}
