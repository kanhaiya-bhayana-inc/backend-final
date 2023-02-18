using Advisor.Core.Domain.DTOS;
using Advisor.Core.Domain.Models;
using Advisor.Core.Interfaces.Services;
using Advisor.Core.Services;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("get-user-auth"),Authorize] // shivam

        public async Task<ActionResult<GetUserDto>> GetUserByAuth()
        {
            var result = await _userService.GetUserByAuth();
            if (result == null) { return NotFound("User doesn't exists."); }
            return Ok(result);
        }

        [HttpPost("add-user")]
        public async Task<ActionResult<Users>> CreateUser(AddUserDto request)
        {
            var result = await _userService.CreateUser(request);
            if (result is null)
            {
                return BadRequest("User already Exist");
            }
            return Ok(result);
        }

        [HttpPost("add-client"),Authorize]
        public async Task<ActionResult<string>> CreateClient(AddUserDto request)
        {
            var result =  _userService.CreateClient(request);
            if (result.Equals("Without advisor client cann't be created.") || result.Equals("Some error occured, try again, or contact advisor.") || result.Equals("this client already created by another advisor, try another one."))
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("user-login")]

        public async Task<ActionResult<string>> Login(AuthAdvisorDto request)
        {
            var result = _userService.Login(request);
            if (result.Equals("Advisor not found.") || result.Equals("Wrong password.") || result.Equals("Not Verified yet"))
                return NotFound(result);

            return Ok(result);
        }

        [HttpPost("verify-user-account")]

        public async Task<ActionResult<string>> AdvisorAccVerify(string token)
        {
            var res = _userService.AdvisorAccVerify(token);
            if (res.Equals("Invalid Token"))
                return BadRequest(res);

            return Ok(res);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateUserDto>> UpdateUser(string id, UpdateUserDto request)
        {
            var result = _userService.UpdateUser(id, request);
            if (result is null)
                return NotFound("Advisor not found.");

            return Ok(result);
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult<string>> ResetPasswordUser(UserResetPasswordDto request)
        {
            var res = _userService.ResetPasswordUser(request);
            if (res.Equals("Token Invalid or expired, try again."))
                return BadRequest(res);
            return Ok(res);
        }

        [HttpPost("forgot-password")]
        public async Task<ActionResult<string>> ForgotPasswordUser(string emial)
        {
            var res = _userService.ForgotPasswordUser(emial);
            if (res.Equals("Advisor not found"))
                return BadRequest(res);
            return Ok(res);
        }

        [HttpGet("change-password"), Authorize]
        public ActionResult<string> ChangePassword()
        {
            //used
            var res = _userService.ChangePassword();
            return Ok(res);
        }

        [HttpGet("get-all-clients/{id}"),Authorize]
        public async Task<ActionResult<List<GetAllClientDTOs>>> GetAllClients(int id)
        {
            var obj = await _userService.GetAllClients(id);
            if (obj == null)
                return NotFound("Advisor Not found");
            return Ok(obj) ;
        }

        [HttpGet("get-all-ids/{id}")]
        public async Task<ActionResult<List<int>>> GetAllIDS(int id)
        {
            return await _userService.GetAllIDS(id);
        }

    }
}
