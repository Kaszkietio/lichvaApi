using API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/dictionary")]
    [ApiController]
    public class DictionaryController : ControllerBase
    {
        public IBankRepository Repository { get; init; }

        public DictionaryController(IBankRepository repo) 
        {
            Repository = repo;
        }

        [HttpGet]
        [Route("jobs")]
        public ActionResult<IEnumerable<string>> GetJobs()
        {
            List<string> jobs = new List<string>();
            jobs.Add("Śmieć");
            jobs.Add("Cwel");
            jobs.Add("Chuj");

            return Ok(jobs);
        }
    }
}
