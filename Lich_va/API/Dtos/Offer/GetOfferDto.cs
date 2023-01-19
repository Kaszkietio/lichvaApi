namespace API.Dtos.Offer
{
    public class GetOfferDto
    {
        public int Id { get; set; }
        public decimal? Percentage { get; set; }
        public decimal? MonthlyInstallment { get; set; } 
        public int? Ammount { get; set; }
        public int? Installments { get; set; }
        public int? StatusId { get; set; }
        public string? StatusDescription { get; set; }
        public int? InquiryId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set;}
        public int? ApprovedBy { get; set; }
        public string? DocumentLink { get; set; }
        public DateTime? DocumentLinkValidDate { get; set; }
        public int? BankId { get; set; }
    }
}
