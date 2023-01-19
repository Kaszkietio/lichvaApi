using API.Dtos.User;
using API.Repositories;
using BankDataLibrary.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        IBankRepository Repository { get; init; }
        public UserController(IBankRepository repo) { Repository = repo; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetUserDto>>> GetAllAsync(
            [FromHeader] string authToken,
            [FromQuery] IList<int> idFilter,
            [FromQuery] IList<DateTime> createDateFilter,
            [FromQuery] IList<string> emailFilter,
            [FromQuery] IList<int> roleFilter,
            [FromQuery] IList<bool> internalFilter,
            [FromQuery] IList<bool> anonymousFilter,
            [FromQuery] IList<string> hashFilter
            )
        {
            try
            {
                await Repository.AuthenticateUserAsync(authToken);
                return Ok((await Repository.GetUsersAsync()).Select(x => x.AsGetDto()));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update(
            [FromHeader] string authToken,
            [FromBody] UpdateUserDto user
            )
        {
            try
            {
                User authUser = await Repository.AuthenticateUserAsync(authToken);
                await Repository.UpdateUserAsync(authUser.Id, user);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }
    }
}
