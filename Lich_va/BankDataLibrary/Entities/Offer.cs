using BankDataLibrary.Config;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankDataLibrary.Entities
{
    [Table("offers", Schema = LichvaContext.SchemaName)]
    public class Offer
    {
        //[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; init; }

        [Column("creation_date")]
        public DateTime? CreationDate { get; init; }

        [Column("inquiry_id")]
        public int? InquiryId { get; set; }

        public decimal? Percentage { get; set; }

        public decimal? MonthlyInstallments { get; set; }   

        public int? Status { get; set; }

        [Column("document_link")]
        public string? DocumentLink { get; set; }
        
        public virtual Inquiry Inquiry { get; set; }
        public virtual OfferStatus OfferStatus { get; set; }
        public virtual ICollection<OfferHistory> History { get; set; }
    }
}
