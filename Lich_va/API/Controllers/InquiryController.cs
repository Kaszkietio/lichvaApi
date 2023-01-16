using API.Dtos;
using API.Repositories;
using BankDataLibrary.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using static API.Repositories.IBankRepository;

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
        public async Task<ActionResult<IEnumerable<InquiryDto>>> GetAll(
            int? inquiryId = null,
            int? userId = null
            )
        {
            return Ok((await Repository.GetInquiriesAsync(inquiryId, userId))
                                       .Select(x => x.AsDto()));
        }

        [HttpGet]
        [Route("{inquiryId}")]
        public async Task<ActionResult<InquiryDto>> Get(int inquiryId)
        {
            Inquiry? res = 
                (await Repository.GetInquiriesAsync(inquiryId, null))
                                 .FirstOrDefault();

            if (res == null)
            {
                return NoContent();
            }

            return Ok(res.AsDto());
        }

        [HttpPost]
        public async Task<ActionResult<InquiryDto>> Post(CreateInquiryDto createInquiryDto)
        {
            Inquiry res = new()
            {
                CreationDate = DateTime.Now,
                UserId = createInquiryDto.UserId,
                Installments = createInquiryDto.Installments,
                Ammount = createInquiryDto.Ammount,
            };

            await Repository.CreateInquiryAsync(res);

            return CreatedAtAction(nameof(Get), new { inquiryId = res.Id }, res.AsDto());
        }
    }
}
