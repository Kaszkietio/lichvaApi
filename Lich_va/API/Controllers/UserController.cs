using API.Dtos;
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
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllAsync()
        {
            return Ok((await Repository.GetUsersAsync()).Select(x => x.AsDto()));
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody]UpdateUserDto user)
        {
            await Repository.UpdateUserAsync(user);
            return Ok();
        }
    }
}
