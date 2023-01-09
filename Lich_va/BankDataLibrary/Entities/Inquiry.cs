﻿using BankDataLibrary.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankDataLibrary.Entities
{
    [Table("inquiries", Schema = LichvaContext.SchemaName)]
    public class Inquiry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public DateTime creation_date { get; set; }
        public int user_id { get; set; }
        public int ammount { get; set; }
        public int installments { get; set; }
    }
}
