using API.Dtos.Dictionary;
using API.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/dictionary")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DictionaryController : ControllerBase
    {
        public IBankRepository Repository { get; init; }

        public DictionaryController(IBankRepository repo)
        {
            Repository = repo;
        }

        [HttpGet]
        [Route("offerStatus")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<DictionaryDto>>> GetOfferStatusAsync()
        {
            return Ok((await Repository.GetOfferStatusesAsync()).Select(x => x.AsDto()));
        }

        [HttpGet]
        [Route("jobs")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<string>>> GetJobsAsync()
        {
            return Ok((await Repository.GetJobTypesAsync()).Select(x => x.AsDto()));
        }

        [HttpGet]
        [Route("roles")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<string>>> GetRolesAsync()
        {
            return Ok((await Repository.GetRolesAsync()).Select(x => x.AsDto()));
        }

        [HttpGet]
        [Route("idTypes")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<string>>> GetIdTypesAsync()
        {
            return Ok((await Repository.GetIdTypesAsync()).Select(x => x.AsDto()));
        }
}
}
