using BankDataLibrary.Entities;
using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class CreateOfferDto
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int UserId { get; init; }
        [Required]
        [Range(0, int.MaxValue)]
        public int BankId { get; init; }
        [Required]
        [Range(0, int.MaxValue)]
        public int PlatformId { get; init; }
        [Required]
        [Range(0, int.MaxValue)]
        public int Ammount { get; init; }
        [Required]
        [Range(0, int.MaxValue)]
        public int Installments { get; init; }
        [Required]
        public string GeneratedContract { get; init; } 
        [Required]
        public string SignedContract { get; init; } 
        [Required]
        public string OfferStatus { get; init; } 
    }
}
