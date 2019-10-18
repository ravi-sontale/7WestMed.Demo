using System.Collections.Generic;

namespace SevenWestMedia.Api.Demo.Services
{
    public interface IUserService
    {
        string GetUserNameById(int id);
        string GetUsersByAge(int age);
        List<string> GetByGendersPerAge();
    }
}
