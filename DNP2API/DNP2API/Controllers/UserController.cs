using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileData;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace DNP2API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController: ControllerBase
    {
        public IUserService UserService;

        public UserController(IUserService UserService)
        {
            this.UserService = UserService;
        }
        [HttpGet]
        public async Task<ActionResult<User>>
            GetFamilies([FromQuery] string? username, [FromQuery] string? password)
        {
            try
            {
                User userToValidate = await UserService.ValidateUser(username, password);
                return Ok(userToValidate);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    }
    
}