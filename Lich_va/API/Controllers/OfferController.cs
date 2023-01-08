using API.Dtos;
using API.Repositories;
using BankDataLibrary.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<OfferDto> Get(int offerId) 
        {
            Offer? offer = Repository.GetOffer(offerId);
            if(offer == null)
            {
                return NotFound();
            }

            return Ok(offer.AsDto());
        }

        [HttpGet]
        public ActionResult<IEnumerable<OfferDto>> GetAll()
        {
            return Ok(Repository.GetOffers().Select(x => x.AsDto()));
        }

        [HttpPost]
        public ActionResult<OfferDto> CreateOffer(CreateOfferDto createOffer)
        {
            Random r = new();
            object? offerStatus;
            if(!Enum.TryParse(typeof(Offer.Status), createOffer.OfferStatus, out offerStatus))
            {
                return BadRequest();
            }

            if(offerStatus == null)
            {
                return BadRequest();
            }

            Offer offer = new()
            {
                Id = r.Next(),
                CreationDate = DateTime.Now,
                UserId = createOffer.UserId,
                BankId = createOffer.BankId,
                PlatformId = createOffer.PlatformId,
                Ammount = createOffer.Ammount,
                Installments = createOffer.Installments,
                GeneratedContract = createOffer.GeneratedContract,
                SignedContract = createOffer.SignedContract,
                OfferStatus = (Offer.Status)offerStatus,
            };

            Repository.CreateOffer(offer);

            return CreatedAtAction(nameof(Get), new { offerId = offer.Id }, offer.AsDto());
        }
    }
}
