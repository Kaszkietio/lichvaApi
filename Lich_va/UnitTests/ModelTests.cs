
using API;
using BankDataLibrary.Entities;

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
                Name = "random2",
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
        public void InquiriesTest()
        {
            using LichvaContext db = new();
            Inquiry test = new Inquiry
            {
                CreationDate = DateTime.Now,
                UserId = 1,
                Ammount = 150,
                Installments = 10,
            };
            db.Inquiries.Add(test);
            db.SaveChanges();

            foreach (Inquiry inq in db.Inquiries)
            {
                Console.WriteLine($"{inq.Id}, {inq.UserId}, {inq.CreationDate}, {inq.Ammount}, {inq.Installments}");
            }

            db.Inquiries.Remove(test);
            db.SaveChanges();
        }

        [TestMethod]
        public void OfferTest()
        {
            using LichvaContext db = new();
            Offer test = new Offer()
            {
                CreationDate = DateTime.Now,
                UserId = 1,
                BankId = 1,
                PlatformId = 2,
                Ammount = 150,
                Installments = 10,
                Status = Category.OfferStatusCaregories.First().Name
            };
            db.Offers.Add(test);
            db.SaveChanges();

            foreach (Offer off in db.Offers)
            {
                Console.WriteLine($"{off.Id}, " +
                    $"{off.UserId}, " +
                    $"{off.CreationDate}, " +
                    $"{off.Ammount}, " +
                    $"{off.Installments}, " +
                    $"{off.Status}," +
                    $"{off.PlatformId}," +
                    $"{off.BankId},");
            }

            db.Offers.Remove(test); 
            db.SaveChanges();
        }

        [TestMethod]
        public void UserTest()
        {
            using LichvaContext db = new();
            User test = new User()
            {
                CreationDate = DateTime.Now,
                Role = Category.UserRoleCategories.First().Name,
                Hash = "h4shT3sT3=2",
                Internal = false,
                Email = "testmail@wowo.com",
                FirstName = "Steve",
                LastName = "Stevens",
                JobType = Category.UserJobCategories.First().Name,
                IncomeLevel = 1000,
                IdType = Category.UserIdTypeCategories.First().Name,
                IdNumber = "",
            };
            db.Users.Add(test);
            db.SaveChanges();

            foreach (User x in db.Users)
            {
                Console.WriteLine(
                    $"{x.Id}, " +
                    $"{x.CreationDate}, " +
                    $"{x.Role}, " +
                    $"{x.Hash}, " +
                    $"{x.Internal}, " +
                    $"{x.Email}, " +
                    $"{x.FirstName}, " +
                    $"{x.LastName}, " +
                    $"{x.JobType}, " +
                    $"{x.IncomeLevel}, " +
                    $"{x.IdType}, " +
                    $"{x.IdNumber}"
                    );
            }

            db.Users.Remove(test);
            db.SaveChanges();
        }
        [TestMethod]
        public void OfferHistoryTest()
        {
            using LichvaContext db = new();
            OfferHistory test = new OfferHistory
            {
                CreationDate = DateTime.Now,
                OfferId = 1,
                NewState = Category.OfferStatusCaregories.First(x => x.Id == 4).Name,
                EmployeeId = 1,
            };
            db.OfferHistories.Add(test);
            db.SaveChanges();

            foreach (OfferHistory x in db.OfferHistories)
            {
                Console.WriteLine(
                    $"{x.Id}, " +
                    $"{x.CreationDate}, " +
                    $"{x.OfferId}, " +
                    $"{x.NewState}, " +
                    $"{x.EmployeeId}"
                    );
            }

            db.OfferHistories.Remove(test);
            db.SaveChanges();
        }
        [TestMethod]
        public void LoginHistoryTest()
        {
            using LichvaContext db = new();
            LoginHistory test = new LoginHistory
            {
                Time = DateTime.Now,
                IP = "127.0.0.1",
                UserId = 1,
            };
            db.LoginHistories.Add(test);
            db.SaveChanges();

            foreach (LoginHistory x in db.LoginHistories)
            {
                Console.WriteLine(
                    $"{x.Id}, " +
                    $"{x.Time}, " +
                    $"{x.UserId}, " +
                    $"{x.Time}"
                    );
            }

            db.LoginHistories.Remove(test);
            db.SaveChanges();
        }
    }
}