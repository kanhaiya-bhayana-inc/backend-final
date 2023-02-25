using Advisor.Core.Domain.DTOS;
using Advisor.Core.Interfaces.Repositories;
using Advisor.Core.Interfaces.Services;
using Advisor.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Advisor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvestmentController : ControllerBase
    {
        private readonly IInvestmentService _investmetnService;

        public InvestmentController(IInvestmentService investmentService )
        {
            _investmetnService= investmentService;
        }

        [HttpPost("advisor-add-investments/{id}"),Authorize]
        public async Task<ActionResult<string>> AddInvestment(InvestmentDTO request, int id)
        {
            var res = await _investmetnService.AddInvestment(request,id);
            if (res.Equals("Some error has occured with investment information, try again."))
            {
                return BadRequest(res);
            }
            return Ok(res);
        }

        [HttpPost("advisorUpdateinvestment/{infoID}/{strtID}/{advId}"), Authorize]
        public async Task<ActionResult<string>> UpdateInvestment(InvestmentDTO request, int infoID, int strtID, int advId)
        {
            var res = await _investmetnService.UpdateInvestment(request, infoID,strtID,advId);
            if (res.Equals("Some error has occured with investment information, try again."))
            {
                return BadRequest(res);
            }
            return Ok(res);
        }

        [HttpDelete("advisorDeleteinvestment/{infoID}/{strtID}"), Authorize]
        public async Task<ActionResult<string>> DeleteInvestment(int infoID, int strtID)
        {
            var res = await _investmetnService.DeleteInvestment(infoID, strtID);
            if (res.Equals("Something went wrong, try again later or contact admin."))
            {
                return BadRequest(res);
            }
            return Ok(res);
        }

        [HttpGet("GetUserInvestments/{uID}"),Authorize]
        public async Task<ActionResult<List<GetInvestments>>> GetuserInvestments(int uID)
        {
            try
            {
                var res = _investmetnService.GetuserInvestments(uID);
                if (res is null)
                {
                    return BadRequest("Something went wrong");
                }
                return Ok(res);
            }
            catch(Exception) { throw; }
        }
    }
}
