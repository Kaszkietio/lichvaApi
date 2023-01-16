using API.Dtos;
using API.Repositories;
using BankDataLibrary.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using System.Runtime.InteropServices;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        IBankRepository Repository { get; init; }

        public OfferController(IBankRepository repository)
        {
            Repository = repository;
        }

        [HttpGet]
        [Route("{offerId}")]
        public async Task<ActionResult<OfferDto>> GetAsync(int offerId)
        {
            Offer? offer = await Repository.GetOfferAsync(offerId);
            if (offer == null)
            {
                return NotFound();
            }

            return Ok(offer.AsDto());
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OfferDto>>> GetAllAsync(
            int? inquiryId = null,
            int? userId = null,
            int? bankId = null
            )
        {
            return Ok((await Repository.GetOffersAsync(inquiryId, userId, bankId)).Select(x => x.AsDto()));
        }

        [HttpPost]
        public async Task<ActionResult<OfferDto>> CreateOfferAsync(CreateOfferDto createOffer)
        {
            Offer offer = new()
            {
                CreationDate = DateTime.Now,
                UserId = createOffer.UserId,
                BankId = createOffer.BankId,
                PlatformId = createOffer.PlatformId,
                Ammount = createOffer.Ammount,
                Installments = createOffer.Installments,
                Status = Category.OfferStatusCaregories.First().Name,
            };

            await Repository.CreateOfferAsync(offer);

            return CreatedAtAction(nameof(GetAsync), new { offerId = offer.Id }, offer.AsDto());
        }
    }
}
