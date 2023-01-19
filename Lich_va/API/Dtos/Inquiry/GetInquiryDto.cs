using API.Dtos.Offer;

namespace API.Dtos.Inquiry
{
    public class GetInquiryDto
    {
        public int? Ammount { get; set; }
        public int? Installments { get; set; }
        public int? StatusId { get; set; }
        public string? StatusDescription { get; set; }
        public GetOfferDto? Offer { get; set; }
    }
}
