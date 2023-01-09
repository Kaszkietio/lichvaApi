using BankDataLibrary.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankDataLibrary.Config
{
    public class LichvaContext : DbContext
    {
        private static string _connectionString = "";
        public static string ConnectionString { get => _connectionString; set => _connectionString = value; }

        public const string SchemaName = "dbo";

        public LichvaContext() : base(GetContextOptions())
        {}

        private static DbContextOptions GetContextOptions()
        {
            DbContextOptionsBuilder result = new();

            result.UseSqlServer(ConnectionString);
            return result.Options;
        }

        // MODELS
        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<Inquiry> Inquiries { get; set; }
    }
}
