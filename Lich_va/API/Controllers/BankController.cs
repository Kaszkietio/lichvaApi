using API.Dtos;
using API.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BankController : ControllerBase
    {
        IBankRepository Repository { get; init; }
        public BankController(IBankRepository repo) { Repository = repo; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BankDto>>> GetAllAsync()
        {
            return Ok((await Repository.GetBanksAsync()).Select(x => x.AsDto()));
        }
    }
}
