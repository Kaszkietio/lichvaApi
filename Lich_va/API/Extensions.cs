using API.Dtos;
using BankDataLibrary.Entities;

namespace API
{
    public static class Extensions
    {
        public static InquiryDto AsDto(this Inquiry inquire)
        {
            InquiryDto result = new()
            {
                Id = inquire.id,
                CreationDate = inquire.creation_date,
                UserId = inquire.user_id,
                Ammount = inquire.ammount,
                Installments = inquire.installments,
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
