using API.Dtos.Inquiry;
using API.Entities;
using API.Helpers;
using API.Repositories;
using BankDataLibrary.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using static API.Repositories.IBankRepository;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class InquiryController : ControllerBase
    {
        public IBankRepository Repository { get; init; }

        public InquiryController(IBankRepository repo)
        {
            Repository = repo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetInquiryDto>>> GetAll(
            [FromHeader] string authToken,
            //[FromQuery] IList<int> idFilter,
            //[FromQuery] IList<DateTime> createDateFilter,
            //[FromQuery] IList<int> ammountFilter,
            //[FromQuery] IList<int> installmentFilter,
            //[FromQuery] IList<int> bankIdFilter
            [FromQuery] string? idFilter,
            [FromQuery] string? createDateFilter,
            [FromQuery] string? ammountFilter,
            [FromQuery] string? installmentFilter,
            [FromQuery] string? bankIdFilter
            )
        {
            try
            {
                User user = await Repository.AuthenticateUserAsync(authToken);
                
                var result = await Repository.GetInquiriesAsync(
                    user, 
                    idFilter.ParseInt(),
                    createDateFilter.ParseDateTime(),
                    ammountFilter.ParseInt(),
                    installmentFilter.ParseInt(), 
                    bankIdFilter.ParseInt()
                    );
                return Ok(result.Select(x => x.AsGetDto()));
            }
            catch (Exception ex) 
            {
                return StatusCode(500, new { ex.Message });
            }

        }

        [HttpGet]
        [Route("{inquiryId}")]
        public async Task<ActionResult<GetInquiryDto>> Get(
            [FromHeader] string authToken,
            int inquiryId
            )
        {
            try
            {
                User user = await Repository.AuthenticateUserAsync(authToken);
                
                Inquiry? res =
                    (await Repository.GetInquiriesAsync(user, idFilter: (false, new List<int> { inquiryId })))
                                     .FirstOrDefault();

                if (res == null)
                {
                    return NoContent();
                }

                return Ok(res.AsGetDto());
            }
            catch (Exception ex )
            {
                return StatusCode(500, new { ex.Message });
            }

        }

        [HttpPost]
        public async Task<ActionResult<OnInquiryCreationDto>> Post(
            [FromHeader] string authToken,
            CreateInquiryDto createInquiryDto
            )
        {
            try
            {
                User user = await Repository.AuthenticateUserAsync(authToken);
                
                var res = await Repository.CreateInquiryAsync(createInquiryDto, user.Id);
                return CreatedAtAction(nameof(Get), new { inquiryId = res.InquireId }, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }
    }
}
