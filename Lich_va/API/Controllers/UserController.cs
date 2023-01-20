using API.Dtos.Inquiry;
using API.Dtos.Offer;
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
            [FromQuery] string? idFilter,
            [FromQuery] string? createDateFilter,
            [FromQuery] string? emailFilter,
            [FromQuery] string? roleFilter,
            [FromQuery] string? internalFilter,
            [FromQuery] string? anonymousFilter,
            [FromQuery] string? hashFilter
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

        [HttpGet]
        [Route("inquiries")]
        public async Task<ActionResult<IEnumerable<GetInquiryDto>>> GetInquiriesAsync(
            [FromHeader] string authToken,
            [FromQuery] string? creationDate,
            [FromQuery] string? ammount,
            [FromQuery] string? installments
            )
        {
            User? user = await Repository.AuthenticateUserAsync(authToken);
            if(user == null)
            {
                return Unauthorized(new {message = "Not authorized user"});
            }
            bool isAuthorized = await Repository.AuthorizeUserAsync(user, "user");
            if (!isAuthorized)
                return Unauthorized(new { message = "Not authorized role" });


            var offers = await Repository.GetUserInquiriesAsync(user);

            
            return Ok(offers.Select(x => x.AsGetDto()));
        }

        [HttpGet]
        [Route("offers")]
        public async Task<ActionResult<IEnumerable<GetOfferDto>>> GetOffersLichvaAsync(
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
            bool isAuthorized = await Repository.AuthorizeUserAsync(user, "user");
            if (!isAuthorized)
                return Unauthorized(new { message = "Not authorized role" });


            var offers = await Repository.GetUserOffersAsync(user);

            var result = offers.FilterOffers(creationDateFilter, requestedValueFilter, installmentsFilter, percentageFilter, monthlyInstallmentsFilter, bankIdFilter, statusIdFitler);


            return Ok(result);
        }

        [HttpGet]
        [Route("count")]
        [AllowAnonymous]
        public async Task<ActionResult> GetUserCount()
        {
            int count = await Repository.GetUsersCount();

            return Ok(new { count });
        }
    }
}
