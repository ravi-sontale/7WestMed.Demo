using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace SevenWestMedia.Api.Demo.Services
{
    public class AuthService : IAuthService
    {
        private readonly IMemoryCache _cache;

        public AuthService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task<string> GetAuthToken(string apiKey)
        {
            return await
                _cache.GetOrCreateAsync(GetCacheKey(apiKey), entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60);
                    return Task.FromResult(Guid.NewGuid().ToString());
                });
        }

        private static string GetCacheKey(string apiKey)
        {
            return $"{apiKey}_SAUTH";
        }
    }
}