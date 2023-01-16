using BankDataLibrary.Config;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankDataLibrary.Entities
{
    [Table("offers", Schema = LichvaContext.SchemaName)]
    public class Offer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; init; }

        [Column("creation_date")]
        public DateTime CreationDate { get; init; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("bank_id")]
        public int BankId { get; set; }

        [Column("platform_id")]
        public int PlatformId { get; set; }

        public int Ammount { get; set; }

        public int Installments { get; set; }

        public string Status { get; set; } = string.Empty;

        public virtual User User { get; set; }

        public virtual Bank Bank { get; set; }

        public virtual Bank Platform { get; set; }

        public virtual ICollection<OfferHistory> History { get; set; }
    }
}
