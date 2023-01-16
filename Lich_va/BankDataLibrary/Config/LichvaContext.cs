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
        private static string _connectionString = "Server = tcp:lichvadbserver.database.windows.net,1433;Initial Catalog = lichvaDB;Persist Security Info=False;User ID = APIuser;Password=\"kurwodzialaj3!\";MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout = 30;";
        public static string ConnectionString { get => _connectionString; set => _connectionString = value; }

        public const string SchemaName = "dbo";

        public LichvaContext() : base(GetContextOptions())
        {}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Offer inf-1 Bank
            builder.Entity<Offer>()
                .HasOne(offer => offer.Bank)
                .WithMany(bank => bank.Offers)
                .HasForeignKey(offer => offer.BankId);

            // Offer inf-1 Bank
            builder.Entity<Offer>()
                .HasOne(offer => offer.Platform)
                .WithMany(bank => bank.PlatformOffers)
                .HasForeignKey(offer => offer.PlatformId);

            // Offer inf-1 User
            builder.Entity<Offer>()
                .HasOne(offer => offer.User)
                .WithMany(user => user.Offers)
                .HasForeignKey(offer => offer.UserId);

            // Inquiry inf-1 User
            builder.Entity<Inquiry>()
                .HasOne(inq => inq.User)
                .WithMany(user => user.Inquiries)
                .HasForeignKey(inq => inq.UserId);

            // OfferHst inf-1 User
            builder.Entity<OfferHistory>()
                .HasOne(hst => hst.Employee)
                .WithMany(user => user.OfferHistory)
                .HasForeignKey(hst => hst.EmployeeId);

            // OfferHst inf-1 Offer
            builder.Entity<OfferHistory>()
                .HasOne(hst => hst.Offer)
                .WithMany(offer => offer.History)
                .HasForeignKey(hst => hst.OfferId);

            // LoginHst inf-1 User
            builder.Entity<LoginHistory>()
                .HasOne(hst => hst.User)
                .WithMany(user => user.Logins)
                .HasForeignKey(hst => hst.UserId);
                
            base.OnModelCreating(builder);
        }

        private static DbContextOptions GetContextOptions()
        {
            DbContextOptionsBuilder result = new();

            result.UseSqlServer(ConnectionString);
            return result.Options;
        }

        // MODELS
        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<Inquiry> Inquiries { get; set; }
        public virtual DbSet<Offer> Offers { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<OfferHistory> OfferHistories { get; set; }
        public virtual DbSet<LoginHistory> LoginHistories { get; set; }
    }
}
