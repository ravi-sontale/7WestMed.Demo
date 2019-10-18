using System.IO;

namespace SevenWestMedia.Api.Demo.Providers
{
    public interface IFileSystemWrapper
    {
        string ReadAllText(string filename);
    }
    public class FileSystemWrapper : IFileSystemWrapper
    {
        public string ReadAllText(string filename)
        {
            return File.ReadAllText(filename);
        }
    }
}
