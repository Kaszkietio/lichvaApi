using API.Dtos;
using API.Repositories;
using BankDataLibrary.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InquireController : ControllerBase
    {
        public IBankRepository Repository { get; init; }

        public InquireController(IBankRepository repo) 
        {
            Repository = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<InquireDto>> GetAll()
        {
            return Ok(Repository.GetInquires().Select(x => x.AsDto()));
        }

        [HttpGet]
        [Route("{inquireId}")]
        public ActionResult<InquireDto> Get(int inquireId)
        {
            Random random= new Random();
            Inquire? res = Repository.GetInquire(inquireId);

            if(res == null)
            {
                return NotFound();
            }

            return Ok(res.AsDto());
        }

        [HttpPost]
        public ActionResult<InquireDto> Post(CreateInquireDto createInquiryDto)
        {
            Random r = new();
            Inquire res = new()
            {
                Id = r.Next(),
                CreationDate = DateTime.Now,
                UserId = createInquiryDto.UserId,
                Installments = createInquiryDto.Installments,
                Ammount = createInquiryDto.Ammount,
            };

            Repository.CreateInquire(res);

            return CreatedAtAction(nameof(Get), new { inquireId = res.Id }, res.AsDto());
        }
    }
}
