using Advisor.Core.Domain.Models;
using Advisor.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Advisor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Users>?>> GetAllUsers()
        {
            return await _userService.GetAllUsers();
        }

        [HttpPost]
        public async Task<ActionResult<Users>> CreateUser(Users ObjUsers)
        {
            var result = await _userService.CreateUser(ObjUsers);
            return Ok(result);
        }
    }
}
