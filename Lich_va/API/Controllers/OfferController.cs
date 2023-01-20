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
        public async Task<ActionResult<GetOfferDto>> GetAsync(
            [FromHeader] string authToken,
            int offerId
            )
        {
            try
            {
                User user = await Repository.AuthenticateUserAsync(authToken);

                Offer? offer = (await Repository.GetOffersAsync(user, idFilter: (false, new List<int> { offerId }))).FirstOrDefault();
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
        public async Task<ActionResult<IEnumerable<GetOfferDto>>> GetAllOffersByInquiryAsync(
            [FromHeader] string authToken,
            int inquiryId
            )
        {
            User? user = await Repository.AuthenticateUserAsync(authToken);
            if (user == null)
                return Unauthorized();

            var offers = await Repository.GetOffersByInquiryAsync(user, inquiryId);

            return Ok(offers.Select(x => x.AsGetDto()));
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
                    idFilter: (false, new List<int> { offerId })))
                    .FirstOrDefault();

                if( offer == null )
                {
                    return BadRequest(new { message = "Invalid offerId" });
                }

                BlobContainerClient client = new(AppSettings.Instance.BlobConnectionString, AppSettings.Instance.BlobContainerName);

                using MemoryStream stream = new();
                await file.CopyToAsync(stream);
                string ext = Path.GetExtension(file.FileName);
                stream.Position = 0;

                string blobName = user.GetContractName(offer, ext);

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
                    idFilter: (false, new List<int> { offerId })))
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

                string blobName = Path.GetFileName(offer.DocumentLink);

                var blob = await client.GetBlobClient(blobName).DownloadContentAsync();
                Stream blobStream = await client.GetBlobClient(blobName).OpenReadAsync();

                return File(blobStream, blob.Value.Details.ContentType, blobName[..^1]);
            }
            catch ( Exception ex )
            {
                return StatusCode(500, new { ex.Message });
            }
        }

        [HttpPut]
        [Route("{offerId}/updateStatus")]
        public async Task<ActionResult> UpdateOfferStatusAsync(
            [FromHeader] string authToken,
            int offerId,
            int newStateId
            )
        {
            try
            {
                User? user = await Repository.AuthenticateUserAsync(authToken);
                if (user == null)
                    return Unauthorized();

                OfferStatus? offerStatus = await Repository.CheckIdStatus(newStateId);
                if (offerStatus == null)
                    return BadRequest(new { message = "Wrong offer status" });

                Offer? offer = (await Repository.GetOffersAsync(user)).FirstOrDefault(x => x.Id == offerId);
                if (offer == null)
                    return NotFound();

                await Repository.UpdateOfferStatus(offer, newStateId);

                EmailSender.SendEmail(user);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }
    }
}
