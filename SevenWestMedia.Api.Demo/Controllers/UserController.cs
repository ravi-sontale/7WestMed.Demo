using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SevenWestMedia.Api.Demo.Services;

namespace SevenWestMedia.Api.Demo.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Retrieve user full name for given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("name/{id}")]
        public ActionResult<string> GetUserNameById([FromRoute]int id)
        {
            return Ok(_userService.GetUserNameById(id));
        }

        /// <summary>
        /// Retrieve all the users first names of given age
        /// </summary>
        /// <param name="age"></param>
        /// <returns></returns>
        [HttpGet("age/{age}")]
        public ActionResult<string> GetUsersByAge([FromRoute]int age)
        {
            return Ok(_userService.GetUsersByAge(age));
        }

        /// <summary>
        /// Retrieve the number of genders per Age, displayed from youngest to oldest
        /// </summary>
        /// <returns></returns>
        [HttpGet("genders/age")]
        public ActionResult<List<string>> GetByGendersPerAge()
        {
            return Ok(_userService.GetByGendersPerAge());
        }
    }
}
