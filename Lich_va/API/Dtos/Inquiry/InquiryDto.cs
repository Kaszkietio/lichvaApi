﻿using System.ComponentModel.DataAnnotations;

namespace API.Dtos.Inquiry
{
    public record InquiryDto
    {
        [Required]
        public int Id { get; init; }
        public DateTime CreationDate { get; init; }
        public int UserId { get; init; }
        public int Ammount { get; init; }
        public int Installments { get; init; }
    }
}
