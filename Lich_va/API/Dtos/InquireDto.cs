using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public record InquireDto
    {
        [Required]
        public int Id { get; init; }
        public DateTime CreationDate { get; init; }
        public int UserId { get; init; }
        public int Ammount { get; init; }
        public int Installments { get; init; } 
    }
}
