using BankDataLibrary.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
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
            // PRIMARY KEYS
            builder.Entity<Bank>()
                .HasKey(bank => bank.Id);

            builder.Entity<ForeignInquiry>()
                .HasKey(finq => new { finq.InquiryId, finq.BankId });

            builder.Entity<IdType>()
                .HasKey(type => type.Id);

            builder.Entity<Inquiry>()
                .HasKey(inq => inq.Id);

            builder.Entity<JobType>()
                .HasKey(type => type.Id);

            builder.Entity<Offer>()
                .HasKey(x => x.Id);

            builder.Entity<OfferHistory>()
                .HasKey(x => x.Id);

            builder.Entity<OfferStatus>()
                .HasKey(offer => offer.Id);

            builder.Entity<Role>()
                .HasKey(role => role.Id);

            builder.Entity<User>()
                .HasKey(user => user.Id);

            // FOREIGN KEYS
            // Offer 1-1 Inq
            builder.Entity<Offer>()
                .HasOne(offer => offer.Inquiry)
                .WithOne(inq => inq.Offer)
                .HasForeignKey<Offer>(offer => offer.InquiryId);

            // Offer 1-1 OfferStatus
            builder.Entity<Offer>()
                .HasOne(offer => offer.OfferStatus)
                .WithMany(status => status.Offers)
                .HasForeignKey(offer => offer.Status);
            
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

            // OfferHst inf-1 OfferStatus
            builder.Entity<OfferHistory>()
                .HasOne(hst => hst.Status)
                .WithMany(status => status.OfferHistories)
                .HasForeignKey(hst => hst.StatusId);

            // ForeignInq 1 - 1 Inq
            builder.Entity<ForeignInquiry>()
                .HasOne(finq => finq.Inquiry)
                .WithOne(inq => inq.ForeignInquiry)
                .HasForeignKey<ForeignInquiry>(finq => finq.InquiryId);

            // ForeingInq 1-1 Bank
            builder.Entity<ForeignInquiry>()
                .HasOne(finq => finq.Bank)
                .WithMany(bank => bank.ForeignInquiries)
                .HasForeignKey(finq => finq.BankId);

            // User inf-1 Role
            builder.Entity<User>()
                .HasOne(user => user.Role)
                .WithMany(role => role.Users)
                .HasForeignKey(user => user.RoleId);

            // User inf-1 JobType
            builder.Entity<User>()
                .HasOne(user => user.Job)
                .WithMany(job => job.Users)
                .HasForeignKey(user => user.JobTypeId);

            // User inf-1 IdType
            builder.Entity<User>()
                .HasOne(user => user.IDType)
                .WithMany(type => type.Users)
                .HasForeignKey(user => user.IdTypeId);

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
        public virtual DbSet<ForeignInquiry> ForeignInquiries { get; set; }
        public virtual DbSet<IdType> IdTypes { get; set; }
        public virtual DbSet<Inquiry> Inquiries { get; set; }
        public virtual DbSet<JobType> JobTypes { get; set; }
        public virtual DbSet<Offer> Offers { get; set; }
        public virtual DbSet<OfferHistory> OfferHistories { get; set; }
        public virtual DbSet<OfferStatus> OfferStatuses { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
