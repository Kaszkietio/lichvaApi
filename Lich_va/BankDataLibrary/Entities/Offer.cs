using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankDataLibrary.Entities
{
    public class Offer
    {
        public enum Status { };
        public Status OfferStatus { get; set; }
    }
}
