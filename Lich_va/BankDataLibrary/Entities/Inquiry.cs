using BankDataLibrary.Config;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankDataLibrary.Entities
{
    [Table("inquiries", Schema = LichvaContext.SchemaName)]
    public class Inquiry
    {
        //[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("creation_date")]
        public DateTime CreationDate { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        public int Ammount { get; set; }

        public int Installments { get; set; }

        public virtual User User { get; set; }
        public virtual Offer Offer { get; set; }
        public virtual ForeignInquiry ForeignInquiry { get; set; }
    }
}
