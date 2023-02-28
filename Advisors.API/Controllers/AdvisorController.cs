using Advisor.Core.Domain.DTOS;
using Advisor.Core.Domain.Models;
using Advisor.Core.Interfaces.Services;
using Advisor.Core.Services;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Advisor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvisorController : ControllerBase
    {
        private readonly IAdvisorService _advisorService;
        private readonly IConfiguration _configuration;

        public AdvisorController(IAdvisorService advisorService, IConfiguration configuration)
        {
            _advisorService = advisorService;
            _configuration = configuration;
        }

        [HttpGet,Authorize(Roles ="Admin")]

        public async Task<ActionResult<List<AdvisorInfoToReturn>>> GetAllAdvisors()
        {
            return await _advisorService.GetAllAdvisors(); 
        }

        [HttpPost]
        public async Task<ActionResult<AdvisorInfo>> CreateAdvisor(AdviserDto request)
        {
            var result = await _advisorService.CreateAdvisor(request); 
            if (result == null)
            {
                return BadRequest("User already exist with this email."); ;
            }
            return Ok("Advisor successfully created!");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AdvisorInfoToReturn>> GetAdvisorById(int id)
        {
            var result = await _advisorService.GetAdvisorById(id);
            if (result is null)
                return NotFound("Advisor not found.");
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<AdvisorInfo>>> DeleteAdvisor(int id)
        {
            var result = await _advisorService.DeleteAdvisor(id);
            if (result is null)
                return NotFound("Advisor not found.");

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AdvisorInfoToReturn>> UpdateAdvisor(int id, AdvisorInfoToReturn request)
        {
            var result = await _advisorService.UpdateAdvisor(id, request);
            if (result is null)
                return NotFound("Advisor not found.");

            return Ok(result);
        }

        [HttpGet("getAuth"), Authorize]
        public ActionResult<string> GetMe()
        {

            // this is done by dependency injection
            var userName = _advisorService.GetMyName();
            return Ok(userName);


            // this is normal
            /*
            var userName = User?.Identity?.Name;
            var userName2 = User.FindFirstValue(ClaimTypes.Name);
            var role = User?.FindFirstValue(ClaimTypes.Role);
            return Ok(new { userName , userName2, role});
            */
        }

        [HttpPost("login")]

        public async Task<ActionResult<string>> Login(AuthAdvisorDto request)
        {
            var result = _advisorService.Login(request);
            if (result.Equals("Advisor not found.") || result.Equals("Wrong password.") || result.Equals("Not Verified yet"))
                return NotFound(result);

            return Ok(result);
        }

        [HttpPost("VerifyAdvisorAccount")]

        public async Task<ActionResult<string>> AdvisorAccVerify(string token)
        {
            var res = _advisorService.AdvisorAccVerify(token);
            if (res.Equals("Invalid Token"))
                return BadRequest(res);

            return Ok(res);
        }


        [HttpPost("forgot-password")]
        public async Task<ActionResult<string>> ForgotPasswordAdv(string emial)
        {
            var res = _advisorService.ForgotPasswordAdv(emial);
            if (res.Equals("Advisor not found"))
                return BadRequest(res);
            return Ok(res);
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult<string>> ResetPasswordAdv(AdvResetPasswordDto request)
        {
            var res = _advisorService.ResetPasswordAdv(request);
            if (res.Equals("Token Invalid or expired, try again."))
                return BadRequest(res);
            return Ok(res);
        }


    }
}
