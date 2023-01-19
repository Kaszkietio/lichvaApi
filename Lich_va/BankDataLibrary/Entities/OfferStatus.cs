using BankDataLibrary.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankDataLibrary.Entities
{
    [Table("offer_status", Schema = LichvaContext.SchemaName)]
    public class OfferStatus
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 

        public string? Name { get; set; }

        public ICollection<Offer> Offers { get; set; }
        public ICollection<OfferHistory> OfferHistories { get; set; }
    }
}
