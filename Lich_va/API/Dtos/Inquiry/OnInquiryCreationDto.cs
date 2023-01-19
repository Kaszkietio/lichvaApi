namespace API.Dtos.Inquiry
{
    public class OnInquiryCreationDto
    {
        public int? InquireId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public GetInquiryDto? Data { get; set; }
    }
}
