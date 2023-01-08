﻿using BankDataLibrary.Entities;

namespace API.Dtos
{
    public record OfferDto
    {
        public int Id { get; init; }
        public DateTime CreationDate { get; init; }
        public int UserId { get; init; }
        public int BankId { get; init; }
        public int PlatformId { get; init; }
        public int Ammount { get; init; }
        public int Installments { get; init; }
        public string GeneratedContract { get; init; } = string.Empty;
        public string SignedContract { get; init; } = string.Empty;
        public string OfferStatus { get; init; } = Offer.Status.BigCycFanclub.ToString();
    }
}