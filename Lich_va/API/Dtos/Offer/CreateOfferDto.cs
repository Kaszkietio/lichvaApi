using BankDataLibrary.Entities;
using System.ComponentModel.DataAnnotations;

namespace API.Dtos.Offer
{
    public class CreateOfferDto
    {
        public DateTime? CreationDate { get; init; }

        public int? InquiryId { get; set; }

        public decimal? Percentage { get; set; }

        public decimal? MonthlyInstallment { get; set; }   

        public int? Status { get; set; }

        public string? DocumentLink { get; set; }    
    }
}
