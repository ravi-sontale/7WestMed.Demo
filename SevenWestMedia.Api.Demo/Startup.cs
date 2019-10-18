using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SevenWestMedia.Api.Demo.Common;
using SevenWestMedia.Api.Demo.Models;
using SevenWestMedia.Api.Demo.Providers;
using SevenWestMedia.Api.Demo.Services;

namespace SevenWestMedia.Api.Demo
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.Configure<ApiSettingsConfig>(Configuration.GetSection("ApiSettings"));
            services.AddSingleton<IConfigProvider, ConfigProvider>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDataProvider<User>, FileDataProvider>();
            services.AddScoped<IFileSystemWrapper, FileSystemWrapper>();
            services.AddScoped<IAuthService, AuthService>();
            
            services.AddLogging();
            services.AddMemoryCache();
            services.AddMvc(options =>
            {
                options.Filters.Add<AuthorizationFilter>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
