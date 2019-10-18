using System.Collections.Generic;

namespace SevenWestMedia.Console.Demo.Repositories
{
    public interface IUserRepository
    {
        string Authenticate(string apiKey);
        string GetUserNameById(int id);
        string GetUsersByAge(int age);
        List<string> GetByGendersPerAge();
    }
}
