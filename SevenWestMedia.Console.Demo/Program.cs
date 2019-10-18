using Microsoft.Extensions.DependencyInjection;
using SevenWestMedia.Console.Demo.Repositories;
using System;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;

namespace SevenWestMedia.Console.Demo
{
    class Program
    {
        private static IServiceProvider _serviceProvider;
        private static IConfiguration _configuration;

        static void Main()
        {
            RegisterServices();

            var userRepository = _serviceProvider.GetService<IUserRepository>();

            //Authenticate the client with API using api key and return token
            var token = userRepository.Authenticate(_configuration["apikey"]);
            if (string.IsNullOrWhiteSpace(token))
            {
                System.Console.WriteLine("User is not authenticated");
                return;
            }

            var id = Convert.ToInt32(_configuration["id"]);

            var userName = userRepository.GetUserNameById(id);
            System.Console.WriteLine(
                !string.IsNullOrWhiteSpace(userName)
                    ? $"The user's full name for id {id} is => {userName}"
                    : $"The user record for id {id} not found");

            var age = Convert.ToInt32(_configuration["age"]);

            var users = userRepository.GetUsersByAge(age);
            System.Console.WriteLine(
                !string.IsNullOrWhiteSpace(users)
                    ? $"All the users first names (comma separated) who are {age} => {users}"
                    : $"The users with age {age} not found");

            var userGendersByAge = userRepository.GetByGendersPerAge();
            if (userGendersByAge != null)
            {
                System.Console.WriteLine("The number of genders per Age, displayed from youngest to oldest =>");
                foreach (var record in userGendersByAge)
                {
                    System.Console.WriteLine(record);
                }
            }

            DisposeServices();
            System.Console.ReadLine();
        }

        /// <summary>
        /// Register services into service collection
        /// </summary>
        private static void RegisterServices()
        {
            var collection = new ServiceCollection();
            collection.AddHttpClient<IUserRepository, UserRepository>(client =>
            {
                client.BaseAddress = new Uri(_configuration["apiUrl"]);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/text"));
            });
            
            _serviceProvider = collection.BuildServiceProvider();

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _configuration  = builder.Build();
        }

        /// <summary>
        /// Dispose all the services which implements IDisposable
        /// </summary>
        private static void DisposeServices()
        {
            if (_serviceProvider == null)
            {
                return;
            }

            if (_serviceProvider is IDisposable)
            {
                ((IDisposable)_serviceProvider).Dispose();
            }
        }
    }
}
