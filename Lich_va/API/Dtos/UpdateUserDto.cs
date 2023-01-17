using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class UpdateUserDto
    {
        [Required]
        public string Role { get; set; } = string.Empty;
        [Required]
        public string Hash { get; set; } = string.Empty;
        [Required]
        public bool Internal { get; set; }
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string JobType { get; set; } = string.Empty;
        [Required]
        public int IncomeLevel { get; set; }
        [Required]
        public string IdType { get; set; } = string.Empty;
        [Required]
        public string IdNumber { get; set; } = string.Empty;
    }
}
