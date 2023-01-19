using System.ComponentModel.DataAnnotations;

namespace API.Dtos.Inquiry
{
    public record PersonalDataDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? BirthDate { get; set; }
    }
    public record DocumentDto
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int TypeId { get; set; }

        public string? Name { get; set; }   
        public string? Description { get; set; }
        public string? Number { get; set; }
    }

    public record JobDetailsDto
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int TypeId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public DateTime? JobStartDate { get; set; }
        public DateTime? JobEndDate { get; set; }
    }

    public record CreateInquiryDto
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Value { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int InstallmentsNumber { get; set; }

        [Required]
        public DocumentDto GovernmentDocument { get; set; }

        [Required]
        public PersonalDataDto PersonalData { get; set; }

        [Required]
        public JobDetailsDto JobDetails { get; set; }
    }
}
