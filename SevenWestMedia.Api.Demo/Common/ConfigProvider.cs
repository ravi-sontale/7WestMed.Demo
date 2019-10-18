using Microsoft.Extensions.Options;

namespace SevenWestMedia.Api.Demo.Common
{
    public class ConfigProvider : IConfigProvider
    {
        private readonly ApiSettingsConfig _settings;

        public ConfigProvider(IOptions<ApiSettingsConfig> settings)
        {
            _settings = settings.Value;
        }

        public string FilePath => _settings.FilePath;
        public string ApiKey => _settings.ApiKey;
    }
}
