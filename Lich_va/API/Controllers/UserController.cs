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
            //[FromQuery] IList<int> idFilter,
            //[FromQuery] IList<DateTime> createDateFilter,
            //[FromQuery] IList<string> emailFilter,
            //[FromQuery] IList<int> roleFilter,
            //[FromQuery] IList<bool> internalFilter,
            //[FromQuery] IList<bool> anonymousFilter,
            //[FromQuery] IList<string> hashFilter
            [FromQuery] string idFilter,
            [FromQuery] string createDateFilter,
            [FromQuery] string emailFilter,
            [FromQuery] string roleFilter,
            [FromQuery] string internalFilter,
            [FromQuery] string anonymousFilter,
            [FromQuery] string hashFilter
            )
        {
            try
            {
                var idFilterS = idFilter.ParseInt();
                var createDateS = createDateFilter.ParseDateTime();
                var emailS = emailFilter.Parse();
                var roleS = roleFilter.ParseInt();
                var internalS = internalFilter.ParseBool();
                var anonymousS = anonymousFilter.ParseBool();
                var hashS = hashFilter.Parse();
                await Repository.AuthenticateUserAsync(authToken);
                return Ok(
                    (await Repository.GetUsersAsync(
                        idFilterS, 
                        createDateS, 
                        roleS, 
                        internalS, 
                        anonymousS, 
                        emailS, 
                        hashS)
                    )
                    .Select(x => x.AsGetDto())
               );
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
