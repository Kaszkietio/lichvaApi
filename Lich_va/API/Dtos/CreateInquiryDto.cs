using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public record CreateInquiryDto
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int UserId { get; init; }
        [Required]
        [Range(0, int.MaxValue)]
        public int Ammount { get; init; }
        [Required]
        [Range(0, int.MaxValue)]
        public int Installments { get; init; } 
    }
}
