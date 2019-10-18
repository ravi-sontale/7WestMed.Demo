using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace SevenWestMedia.Api.Demo.Common
{
    public class AuthorizationFilter : IAuthorizationFilter
    {
        private readonly IMemoryCache _cache;
        private readonly IConfigProvider _configProvider;

        public AuthorizationFilter(IMemoryCache cache, IConfigProvider configProvider)
        {
            _cache = cache;
            _configProvider = configProvider;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!(context.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor))
            {
                return;
            }

            if (controllerActionDescriptor.ControllerName == "Auth")
            {
                var apiKey = context.HttpContext.Request.Headers.Where(a => a.Key == "apikey").Select(a => a.Value).FirstOrDefault();
                if (string.IsNullOrWhiteSpace(apiKey) || apiKey.ToString() !=_configProvider.ApiKey)
                {
                    context.Result = new UnauthorizedResult();
                }
            }
            else
            {
                var token = context.HttpContext.Request.Headers.Where(a => a.Key == "auth-token").Select(a => a.Value).FirstOrDefault();
                if (string.IsNullOrWhiteSpace(token))
                {
                    context.Result = new UnauthorizedResult();
                }

                if (!_cache.TryGetValue($"{_configProvider.ApiKey}_SAUTH", out string cachedToken))
                {
                    context.Result = new UnauthorizedResult();
                }

                if (cachedToken != token)
                {
                    context.Result = new UnauthorizedResult();
                }
            }
        }
    }
}