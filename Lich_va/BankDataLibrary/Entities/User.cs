using BankDataLibrary.Config;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankDataLibrary.Entities
{
    [Table("users", Schema = LichvaContext.SchemaName)]
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("creation_date")]
        public DateTime? CreationDate { get; set; }

        [Column("role")]
        public int? RoleId { get; set; } 

        public string? Hash { get; set; } 

        public bool? Internal { get; set; }

        public bool? Anonymous { get; set; }

        public string? Email { get; set; } 

        [Column("first_name")]
        public string? FirstName { get; set; } 

        [Column("last_name")]
        public string? LastName { get; set; } 

        [Column("job_type")]
        public int? JobTypeId { get; set; } 

        [Column("income_level")]
        public int? IncomeLevel { get; set; }

        [Column("id_type")]
        public int? IdTypeId { get; set; } 

        [Column("id_number")]
        public string? IdNumber { get; set; }

        public virtual IdType IdType { get; set; }
        public virtual JobType JobType { get; set; }
        public virtual Role Role { get; set; }

        public virtual ICollection<Inquiry> Inquiries { get; set; }
        public virtual ICollection<OfferHistory> OfferHistory { get; set; }
    }
}
