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
        public async Task<ActionResult<IEnumerable<string>>> GetOfferStatusAsync()
        {
            IEnumerable<string> offertStatus = Category.OfferStatusCaregories.Select(cat => cat.Name);
            return Ok(offertStatus);
        }

        [HttpGet]
        [Route("jobs")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<string>>> GetJobsAsync()
        {
            IEnumerable<string> jobs = Category.UserJobCategories.Select(cat => cat.Name);
            return Ok(jobs);
        }

        [HttpGet]
        [Route("roles")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<string>>> GetRolesAsync()
        {
            IEnumerable<string> roles = Category.UserRoleCategories.Select(cat => cat.Name);
            return Ok(roles);
        }

        [HttpGet]
        [Route("idTypes")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<string>>> GetIdTypesAsync()
        {
            IEnumerable<string> types = Category.UserIdTypeCategories.Select(cat => cat.Name);
            return Ok(types);
        }
}
}
