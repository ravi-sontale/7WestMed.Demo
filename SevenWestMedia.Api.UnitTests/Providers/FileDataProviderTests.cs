using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using SevenWestMedia.Api.Demo.Common;
using SevenWestMedia.Api.Demo.Models;
using SevenWestMedia.Api.Demo.Providers;
using Xunit;

namespace SevenWestMedia.Api.UnitTests.Providers
{
    public class FileDataProviderTests
    {
        private readonly IDataProvider<User> _target;
        private readonly IConfigProvider _configProvider;
        private readonly ILogger<FileDataProvider> _logger;
        private readonly IFileSystemWrapper _fileSystemWrapper;

        public FileDataProviderTests()
        {
            _configProvider = Substitute.For<IConfigProvider>();
            _logger = Substitute.For<ILogger<FileDataProvider>>();
            _fileSystemWrapper = Substitute.For<IFileSystemWrapper>();
            _target = new FileDataProvider(_configProvider, _logger, _fileSystemWrapper);
        }

        [Fact]
        public void Provide_should_return_valid_data()
        {
            // Arrange
            _configProvider.FilePath.Returns("FilePath");
            _fileSystemWrapper.ReadAllText("FilePath")
                .Returns(
                    "[{\"id\": 53,\"first\" : \"ravi\", \"last\" : \"sontale\",\"age\" : 35,\"gender\" : \"M\"}]");
            // Act
            var result = _target.Provide();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(new List<User>
                {new User {Age = 35, Gender = 'M', Id = 53, First = "ravi", Last = "sontale"}});
        }

        [Fact]
        public void Provide_should_log_and_throw_exception_when_file_reading_fails()
        {
            // Arrange
            var ex = new FileNotFoundException("File not found");
            _configProvider.FilePath.Returns("FilePath");
            _fileSystemWrapper.ReadAllText("FilePath").Throws(ex);

            // Act
            Assert.Throws<FileNotFoundException>(() => _target.Provide());

            // Assert
            _fileSystemWrapper.Received(1).ReadAllText("FilePath");
        }
    }
}
