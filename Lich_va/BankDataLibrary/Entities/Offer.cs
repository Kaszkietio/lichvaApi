using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankDataLibrary.Entities
{
    public class Offer
    {
        public enum Status : int
        {
            BigCycFanclub,
        };
        public int Id { get; init; }
        public DateTime CreationDate { get; init; }
        public int UserId { get; init; }
        public int BankId { get; init; }
        public int PlatformId { get; init; }
        public int Ammount { get; init; }
        public int Installments { get; init; }
        public string GeneratedContract { get; init; } = string.Empty;
        public string SignedContract { get; init; } = string.Empty;
        public Status OfferStatus { get; set; }
    }
}
