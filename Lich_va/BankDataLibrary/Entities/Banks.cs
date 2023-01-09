using BankDataLibrary.Config;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankDataLibrary.Entities
{
    [Table("banks", Schema = LichvaContext.SchemaName)]
    public class Bank
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public DateTime creation_date { get; set; }

        public string name { get; set; }

    }
}
