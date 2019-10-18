using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SevenWestMedia.Api.Demo.Common;
using SevenWestMedia.Api.Demo.Models;

namespace SevenWestMedia.Api.Demo.Providers
{
    /// <summary>
    /// File Data Provider 
    /// </summary>
    public class FileDataProvider : IDataProvider<User>
    {
        private readonly IConfigProvider _configProvider;
        private readonly ILogger<FileDataProvider> _logger;
        private readonly IFileSystemWrapper _fileSystemWrapper;

        public FileDataProvider(IConfigProvider configProvider, ILogger<FileDataProvider> logger, IFileSystemWrapper fileSystemWrapper)
        {
            _configProvider = configProvider;
            _logger = logger;
            _fileSystemWrapper = fileSystemWrapper;
        }

        public List<User> Provide()
        {
            try
            {
                var users = JsonConvert.DeserializeObject<List<User>>(_fileSystemWrapper.ReadAllText(_configProvider.FilePath));
                return users;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while reading the file {ex}");
                throw ex;
            }
        }
    }
}
