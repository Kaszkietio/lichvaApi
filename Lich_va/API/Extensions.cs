using API.Dtos;
using BankDataLibrary.Entities;

namespace API
{
    public static class Extensions
    {
        public static InquireDto AsDto(this Inquire inquire)
        {
            InquireDto result = new()
            {
                Id = inquire.Id,
                CreationDate = inquire.CreationDate,
                UserId = inquire.UserId,
                Ammount = inquire.Ammount,
                Installments = inquire.Installments,
            };
            return result;
        }
        public static OfferDto AsDto(this Offer offer)
        {
            OfferDto result = new()
            {
                Id = offer.Id,
                CreationDate = offer.CreationDate,
                UserId = offer.UserId,
                Ammount = offer.Ammount,
                Installments = offer.Installments,
                BankId = offer.BankId,
                GeneratedContract = offer.GeneratedContract,
                OfferStatus = offer.OfferStatus.ToString(),
                PlatformId = offer.PlatformId,
                SignedContract = offer.SignedContract,
            };
            return result;
        }
    }
}
