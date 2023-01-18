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
    [Table("foreign_inquiries", Schema = LichvaContext.SchemaName)]
    public class ForeignInquiry
    {
        //[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("bank_id")]
        public int BankId { get; set; }

        [Column("inquiry_id")]
        public int InquiryId { get; set; }

        [Column("their_inquiry_id")]
        public int? ForeignInquiryId { get; set; }  

        public virtual Inquiry Inquiry { get; set; }
        public virtual Bank Bank { get; set; }
    }
}
