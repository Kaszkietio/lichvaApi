using API.Dtos.Inquiry;
using API.Dtos.Offer;
using API.Entities;
using API.Helpers;
using API.Repositories;
using Azure.Storage.Blobs;
using BankDataLibrary.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using System.Runtime.InteropServices;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OfferController : ControllerBase
    {
        IBankRepository Repository { get; init; }

        public OfferController(IBankRepository repository)
        {
            Repository = repository;
        }

        [HttpGet]
        [Route("{offerId}")]
        public async Task<ActionResult<OfferDto>> GetAsync(
            [FromHeader] string authToken,
            int offerId
            )
        {
            try
            {
                User user = await Repository.AuthenticateUserAsync(authToken);

                Offer? offer = (await Repository.GetOffersAsync(user, idFilter: new List<int> { offerId })).FirstOrDefault();
                if (offer == null)
                {
                    return NotFound();
                }

                return Ok(offer.AsGetDto());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OfferDto>>> GetAllAsync(
            [FromHeader] string authToken,
            [FromQuery] IList<int> idFilter,
            [FromQuery] IList<int> inquiryIdFilter,
            [FromQuery] IList<DateTime> createDateFilter,
            [FromQuery] IList<decimal> percentageFilter,
            [FromQuery] IList<decimal> monthlyInstallmentFilter,
            [FromQuery] IList<int> statusFilter
            )
        {
            try
            {
                User user = await Repository.AuthenticateUserAsync(authToken);
                var result = await Repository.GetOffersAsync(user,
                    idFilter,
                    inquiryIdFilter,
                    createDateFilter,
                    percentageFilter,
                    monthlyInstallmentFilter,
                    statusFilter
                    );

                return Ok(result.Select(x => x.AsGetDto()));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }

        [HttpPost]
        [Route("{offerId}/document/upload")]
        public async Task<ActionResult> UploadDocument(
            [FromHeader] string authToken,
            int offerId,
            IFormFile file
            )
        {
            try
            {
                User user = await Repository.AuthenticateUserAsync(authToken);
                Offer? offer = (await Repository.GetOffersAsync(
                    user, 
                    idFilter: new List<int> { offerId }))
                    .FirstOrDefault();

                if( offer == null )
                {
                    return BadRequest(new { message = "Invalid offerId" });
                }

                BlobContainerClient client = new(AppSettings.Instance.BlobConnectionString, AppSettings.Instance.BlobContainerName);

                using MemoryStream stream = new();
                await file.CopyToAsync(stream);
                stream.Position = 0;

                // TODO:
                string blobName = user.GetContractName(offer);

                await client.UploadBlobAsync(blobName, stream);

                UpdateOfferDto newInq = new()
                {
                    Status = offer.StatusId,
                    DocumentLink = AppSettings.Instance.BlobUrl + blobName,
                };

                await Repository.UpdateOfferAsync(offer.Id, newInq);

                return Ok(new { message = "Successfully uploaded files" });
            }
            catch ( Exception ex )
            {
                return StatusCode(500, new { ex.Message });
            }
        }

        [HttpGet]
        [Route("{offerId}/document")]
        public async Task<ActionResult<IFormFile>> GetDocument(
            [FromHeader] string authToken,
            int offerId
            )
        {
            try
            {
                User user = await Repository.AuthenticateUserAsync(authToken);
                Offer? offer = (await Repository.GetOffersAsync(
                    user, 
                    idFilter: new List<int> { offerId }))
                    .FirstOrDefault();

                if (offer == null)
                {
                    return BadRequest(new { message = "Invalid offerId" });
                }
                if (offer.DocumentLink == null)
                {
                    return NoContent();
                }
                
                BlobContainerClient client = new(AppSettings.Instance.BlobConnectionString, AppSettings.Instance.BlobContainerName);

                // TODO:
                string blobName = user.GetContractName(offer);

                var blob = await client.GetBlobClient(blobName).DownloadContentAsync();
                Stream blobStream = await client.GetBlobClient(blobName).OpenReadAsync();

                return File(blobStream, blob.Value.Details.ContentType, blobName);
            }
            catch ( Exception ex )
            {
                return StatusCode(500, new { ex.Message });
            }
        }
    }
}
