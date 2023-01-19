using BankDataLibrary.Config;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankDataLibrary.Entities
{
    [Table("banks", Schema = LichvaContext.SchemaName)]
    public class Bank
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("creation_date")]
        public DateTime? CreationDate { get; set; }

        public string? Name { get; set; }

        public virtual ICollection<ForeignInquiry> ForeignInquiries { get; set; }
    }
}
