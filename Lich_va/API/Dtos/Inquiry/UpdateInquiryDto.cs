using System.ComponentModel.DataAnnotations;

namespace API.Dtos.Inquiry
{
    public record UpdateInquiryDto
    {
        [Range(1, int.MaxValue)]
        public int Value { get; set; }

        [Range(1, int.MaxValue)]
        public int InstallmentsNumber { get; set; }
    }
}
