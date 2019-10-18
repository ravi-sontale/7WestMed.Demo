using System.Collections.Generic;
using System.Linq;
using SevenWestMedia.Api.Demo.Models;
using SevenWestMedia.Api.Demo.Providers;

namespace SevenWestMedia.Api.Demo.Services
{
    public class UserService : IUserService
    {
        private readonly IDataProvider<User> _dataProvider;

        public UserService(IDataProvider<User> dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public string GetUserNameById(int id)
        {
            var userData = _dataProvider.Provide();
            return userData.Where(a => a.Id == id).Select(a => a.First + " " + a.Last).FirstOrDefault();
        }

        public string GetUsersByAge(int age)
        {
            var userData = _dataProvider.Provide();
            return string.Join(",", userData.Where(a => a.Age == age).Select(a => a.First));
        }

        public List<string> GetByGendersPerAge()
        {
            var userData = _dataProvider.Provide();

            var query = from item in userData
                group item by new { item.Age, item.Gender }
                into grouped
                select new
                {
                    Age = grouped.Key.Age,
                    Gender = grouped.Key.Gender,
                    Count = grouped.Count()
                };

            var lookup = query.ToLookup(result => result.Age,
                result => new { result.Gender, result.Count });

            return (from result in lookup
                let genderCountsWithAge = $"Age : {result.Key}"
                select result.Aggregate(genderCountsWithAge,
                    (current, subResult) => current + $" {subResult.Gender}: {subResult.Count}")).ToList();
        }
    }
}
