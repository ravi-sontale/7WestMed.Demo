using System.Threading.Tasks;

namespace SevenWestMedia.Api.Demo.Services
{
    public interface IAuthService
    {
        Task<string> GetAuthToken(string apiKey);
    }
}
