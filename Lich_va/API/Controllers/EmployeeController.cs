using API.Dtos.Inquiry;
using API.Dtos.Offer;
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
    public class EmployeeController : ControllerBase
    {
        IBankRepository Repository { get; init; }
        public EmployeeController(IBankRepository repo) { Repository = repo; }

        [HttpGet]
        [Route("inquiries")]
        public async Task<ActionResult<IEnumerable<GetInquiryDto>>> GetEmployeeInquiries(
            [FromHeader] string authToken,
            [FromQuery] string? creationDateFilter,
            [FromQuery] string? ammountFilter,
            [FromQuery] string? installmentsFilter
            )
        {
            User? user = await Repository.AuthenticateUserAsync(authToken);
            if(user == null)
            {
                return Unauthorized(new {message = "Not authorized user"});
            }
            bool isAuthorized = await Repository.AuthorizeUserAsync(user, "employee");
            if (!isAuthorized)
                return Unauthorized(new { message = "Not authorized role" });


            var offers = await Repository.GetEmployeeInquiryAsync(user);
            var tmp = offers.Select(x => x.AsGetDto());
            var result = tmp.FilterInquiries(creationDateFilter, ammountFilter, installmentsFilter);
            return Ok(result);
        }

        [HttpGet]
        [Route("offers")]
        public async Task<ActionResult<IEnumerable<GetOfferDto>>> GetEmployeeOffers(
            [FromHeader] string authToken,
            [FromQuery] string? creationDateFilter,
            [FromQuery] string? requestedValueFilter,
            [FromQuery] string? installmentsFilter,
            [FromQuery] string? percentageFilter,
            [FromQuery] string? monthlyInstallmentsFilter,
            [FromQuery] string? bankIdFilter,
            [FromQuery] string? statusIdFitler
            )
        {
            User? user = await Repository.AuthenticateUserAsync(authToken);
            if(user == null)
            {
                return Unauthorized(new {message = "Not authorized user"});
            }
            bool isAuthorized = await Repository.AuthorizeUserAsync(user, "employee");
            if (!isAuthorized)
                return Unauthorized(new { message = "Not authorized role" });


            var offers = await Repository.GetEmployeeOffersAsync(user);
            var tmp = offers.Select(x => x.AsGetDto());
            var result = tmp.FilterOffers(creationDateFilter, requestedValueFilter, installmentsFilter, percentageFilter, monthlyInstallmentsFilter, bankIdFilter, statusIdFitler);

            return Ok(result);
        }
    }
}
