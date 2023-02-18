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
            if (res.Equals("Some error has occured, try again."))
            {
                return BadRequest(res);
            }
            return Ok(res);
        }
    }
}
