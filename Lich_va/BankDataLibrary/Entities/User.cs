using BankDataLibrary.Config;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankDataLibrary.Entities
{
    [Table("users", Schema = LichvaContext.SchemaName)]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("creation_date")]
        public DateTime CreationDate { get; set; }

        public string Role { get; set; } = string.Empty;

        public string Hash { get; set; } = string.Empty;

        public bool Internal { get; set; }

        public string Email { get; set; } = string.Empty;

        [Column("first_name")]
        public string FirstName { get; set; } = string.Empty;

        [Column("last_name")]
        public string LastName { get; set; } = string.Empty;

        [Column("job_type ")]
        public string JobType { get; set; } = string.Empty;

        [Column("income_level")]
        public int IncomeLevel { get; set; }

        [Column("id_type")]
        public string IdType { get; set; } = string.Empty;

        [Column("id_number")]
        public string IdNumber { get; set; } = string.Empty;

        public virtual ICollection<Inquiry> Inquiries { get; set; }
        public virtual ICollection<Offer> Offers { get; set; }
        public virtual ICollection<OfferHistory> OfferHistory { get; set; }
        public virtual ICollection<LoginHistory> Logins { get; set; }
    }
}
