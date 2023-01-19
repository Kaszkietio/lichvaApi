
using BankDataLibrary.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace UnitTests
{
    [TestClass]
    public class LichvaContextTests
    {
        [TestMethod]
        public void BankTest()
        {
            LichvaContext db = new LichvaContext();
            Bank test = new Bank
            {
                CreationDate = DateTime.Now,
                Name = "Lichva",
            };
            db.Banks.Add(test);
            db.SaveChanges();

            foreach (Bank bank in db.Banks)
            {
                Console.WriteLine($"{bank.Id}, {bank.Name}, {bank.CreationDate}");
            }

            db.Banks.Remove(test);
            db.SaveChanges();
        }

        [TestMethod]
        public void ForeignInquiryTest()
        {
            using LichvaContext db = new();

            Inquiry tmp = new Inquiry
            {
                CreationDate = DateTime.Now,
                UserId = db.Users.First().Id,
                Ammount = 1,
                Installments = 1,
            };

            db.Inquiries.Add(tmp);
            db.SaveChanges();

            ForeignInquiry test = new ForeignInquiry
            {
                BankId = db.Banks.First().Id,
                InquiryId = tmp.Id,
                ForeignInquiryId = null,
            };

            db.ForeignInquiries.Add(test);
            db.SaveChanges();

            foreach(var fq in db.ForeignInquiries)
            {
                Console.WriteLine(
                    $"{fq.Id} " + 
                    $"{fq.BankId} " + 
                    $"{fq.InquiryId} " + 
                    $"{fq.ForeignInquiryId}"
                    );
            }
        }

        [TestMethod]
        public void IdTypeTest()
        {
            using LichvaContext db = new();
            foreach(var type in db.IdTypes)
            {
                Console.WriteLine($"{type.Id}:{type.Name}");
            }
        }

        [TestMethod]
        public void InquiriesTest()
        {
            using LichvaContext db = new();
            Inquiry test = new Inquiry
            {
                CreationDate = DateTime.Now,
                UserId = db.Users.First().Id,
                Ammount = 150,
                Installments = 10,
            };
            db.Inquiries.Add(test);
            db.SaveChanges();

            foreach (Inquiry inq in db.Inquiries)
            {
                Console.WriteLine($"{inq.Id}, {inq.UserId}, {inq.CreationDate}, {inq.Ammount}, {inq.Installments}");
            }

            //db.Inquiries.Remove(test);
            //db.SaveChanges();
        }

        [TestMethod]
        public void JobTypeTest()
        {
            using LichvaContext db = new();

            foreach(var type in db.JobTypes)
            {
                Console.WriteLine($"{type.Id}:{type.Name}:{type.Description}");
            }
        }

        [TestMethod]
        public void OfferTest()
        {
            using LichvaContext db = new();
            Offer test = new Offer()
            {
                CreationDate = DateTime.Now,
                InquiryId = db.Inquiries.First().Id,
                Percentage = 100,
                MonthlyInstallment = 10,
                StatusId = 1,
                DocumentLink = null,
            };
            db.Offers.Add(test);
            db.SaveChanges();

            foreach (Offer off in db.Offers)
            {
                Console.WriteLine(
                    $"{off.Id} " +
                    $"{off.CreationDate} " +
                    $"{off.InquiryId} " + 
                    $"{off.Percentage} " + 
                    $"{off.MonthlyInstallment} " + 
                    $"{off.StatusId} " + 
                    $"{off.DocumentLink} "
                    );
            }

            //db.Offers.Remove(test); 
            //db.SaveChanges();
        }
        [TestMethod]
        public void OfferHistoryTest()
        {
            using LichvaContext db = new();

            int? offerid = db.Offers.First().Id;
            int? statusid = db.OfferStatuses.First().Id;
            OfferHistory test = new OfferHistory
            {
                CreationDate = DateTime.Now,
                OfferId = offerid,
                StatusId = statusid,
                EmployeeId = db.Users.First().Id,
            };
            db.OfferHistories.Add(test);
            db.SaveChanges();

            foreach (OfferHistory x in db.OfferHistories)
            {
                Console.WriteLine(
                    $"{x.Id}, " +
                    $"{x.CreationDate}, " +
                    $"{x.OfferId}, " +
                    $"{x.StatusId}, " +
                    $"{x.EmployeeId}"
                    );
            }

            //db.OfferHistories.Remove(test);
            //db.SaveChanges();
        }

        [TestMethod]
        public void RoleTest()
        {
            using LichvaContext db = new();
            foreach(var role in db.Roles)
            {
                Console.WriteLine($"{role.Id}:{role.Name}");
            }
        }

        [TestMethod]
        public void UserTest()
        {
            using LichvaContext db = new();
            User test = new User()
            {
                CreationDate = DateTime.Now,
                RoleId = db.Roles.First().Id,
                Hash = null,
                Internal = false,
                Anonymous= false,
                Email = null,
                FirstName = null,
                LastName = null,
                JobTypeId = db.JobTypes.First().Id,
                IncomeLevel = null,
                IdTypeId = db.IdTypes.First().Id,
                IdNumber = null,
            };
            db.Users.Add(test);
            db.SaveChanges();

            foreach (User x in db.Users)
            {
                Console.WriteLine(
                    $"{x.Id}, " +
                    $"{x.CreationDate}, " +
                    $"{x.RoleId}, " +
                    $"{x.Hash}, " +
                    $"{x.Internal}, " +
                    $"{x.Email}, " +
                    $"{x.FirstName}, " +
                    $"{x.LastName}, " +
                    $"{x.JobTypeId}, " +
                    $"{x.IncomeLevel}, " +
                    $"{x.IdTypeId}, " +
                    $"{x.IdNumber}"
                    );
            }

            //db.Users.Remove(test);
            //db.SaveChanges();
        }

    }
}