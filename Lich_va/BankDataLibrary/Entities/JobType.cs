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
    [Table("job_types", Schema = LichvaContext.SchemaName)]
    public class JobType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public string? Name { get; set; }   

        public string? Description { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
