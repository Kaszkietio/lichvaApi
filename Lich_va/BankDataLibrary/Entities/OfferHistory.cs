using BankDataLibrary.Config;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankDataLibrary.Entities
{
    [Table("offer_history", Schema = LichvaContext.SchemaName)]
    public class OfferHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("creation_date")]
        public DateTime CreationDate { get; set; }

        [Column("offer_id")]
        public int OfferId { get; set; }

        [Column("new_state")]
        public string NewState { get; set; } = string.Empty;

        [Column("employee_id")]
        public int EmployeeId { get; set; }

        public virtual Offer Offer { get; set; }
        public virtual User Employee { get; set; }
    }
}
