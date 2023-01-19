using BankDataLibrary.Entities;

namespace API.Dtos.Offer
{
    public record OfferDto
    {
        public DateTime? CreationDate { get; init; }

        public int? InquiryId { get; set; }

        public decimal? Percentage { get; set; }

        public decimal? MonthlyInstallment { get; set; }

        public int? Status { get; set; }

        public string? DocumentLink { get; set; }
    }
}
