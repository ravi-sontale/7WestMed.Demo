namespace SevenWestMedia.Api.Demo.Common
{
    public interface IConfigProvider
    {
        string FilePath { get; }
        string ApiKey { get; }
    }
}
