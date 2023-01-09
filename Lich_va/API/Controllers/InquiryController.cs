using API.Dtos;
using API.Repositories;
using BankDataLibrary.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InquiryController : ControllerBase
    {
        public IBankRepository Repository { get; init; }

        public InquiryController(IBankRepository repo) 
        {
            Repository = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<InquiryDto>> GetAll()
        {
            return Ok(Repository.GetInquires().Select(x => x.AsDto()));
        }

        [HttpGet]
        [Route("{inquiryId}")]
        public ActionResult<InquiryDto> Get(int inquiryId)
        {
            Random random= new Random();
            Inquiry? res = Repository.GetInquiry(inquiryId);

            if(res == null)
            {
                return NotFound();
            }

            return Ok(res.AsDto());
        }

        [HttpPost]
        public ActionResult<InquiryDto> Post(CreateInquiryDto createInquiryDto)
        {
            Random r = new();
            Inquiry res = new()
            {
                creation_date = DateTime.Now,
                user_id = createInquiryDto.UserId,
                installments = createInquiryDto.Installments,
                ammount = createInquiryDto.Ammount,
            };

            Repository.CreateInquiry(res);

            return CreatedAtAction(nameof(Get), new { inquiryId = res.id }, res.AsDto());
        }
    }
}
