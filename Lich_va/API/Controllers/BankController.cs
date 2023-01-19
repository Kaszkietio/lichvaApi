using API.Dtos.Bank;
using API.Repositories;
using BankDataLibrary.Entities;
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
        public async Task<ActionResult<IEnumerable<GetBankDto>>> GetAllAsync(
            [FromHeader] string authToken,
            [FromQuery] IList<string> names
            )
        {
            try
            {
                await Repository.AuthenticateUserAsync(authToken);
                var banks = await Repository.GetBanksAsync(names);
                return Ok(banks.Select(x => x.AsGetDto()));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }
    }
}
