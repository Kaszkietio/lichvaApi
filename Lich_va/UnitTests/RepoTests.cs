using API;
using API.Repositories;
using BankDataLibrary.Entities;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class RepoTests
    {
        [TestMethod]
        public void TestBank()
        {
            DBBankRepository repo = new DBBankRepository();
            Bank? bank = repo.GetBankAsync(1).Result;
            var banks = repo.GetBanksAsync().Result;
        }

        [TestMethod]
        public void TestInquiry()
        {
            DBBankRepository repo = new DBBankRepository();
            Inquiry inq = new Inquiry()
            {
                 UserId= 1,
                  Ammount= 1,
                   CreationDate= DateTime.Now, 
                    Installments = 1
            };
            repo.CreateInquiryAsync(inq).Wait();

            var inq2 = repo.GetInquiriesAsync(inq.Id).Result;
            foreach(var i in inq2)
            {
                Console.WriteLine(i.Id);
            }
        }

        [TestMethod]
        public void TestLoginHistory()
        {
            var repo = new DBBankRepository();
            LoginHistory l = new LoginHistory()
            {
                UserId = 1,
                IP = "127.0.0.1",
                Time = DateTime.Now,
            };

            repo.CreateLoginHistoryAsync(l).Wait();

            LoginHistory? res = repo.GetLoginHistoryAsync(l.Id).Result;

            Console.WriteLine(res);

            var arr = repo.GetLoginHistoriesAsync().Result;
            foreach(var log in arr)
            {
                Console.WriteLine(log.Id);
            }
        }

        [TestMethod]
        public void TestOffer()
        {
            var repo = new DBBankRepository();
            Offer l = new Offer()
            {
                CreationDate = DateTime.Now,
                UserId = 1,
                BankId = 1,
                PlatformId = 2,
                Ammount = 10,
                Installments = 23,
                Status = Category.OfferStatusCaregories.First().Name

            };

            repo.CreateOfferAsync(l).Wait();

            var r = repo.GetOfferAsync(l.Id).Result;
            Console.WriteLine(r);

            var arr = repo.GetOffersAsync().Result;

            foreach(var offer in arr) { Console.WriteLine(offer); }
        }

        [TestMethod]
        public void TestOfferHistory()
        {
            var repo = new DBBankRepository();
            OfferHistory l = new OfferHistory()
            {
                CreationDate = DateTime.Now,
                EmployeeId = 1,
                OfferId = 1,
                NewState = Category.OfferStatusCaregories.First(x => x.Id == 4).Name,
            };

            repo.CreateOfferHistoryAsync(l).Wait();
            var r = repo.GetOfferHistoryAsync(l.Id).Result;
            foreach(var off in r) { Console.WriteLine(off); }
        }

        [TestMethod]
        public void TestUser()
        {
            var repo = new DBBankRepository();
            var l = new User()
            {
                 CreationDate= DateTime.Now,
                 Internal = false,
                 IncomeLevel = 0,
                 Role = Category.UserRoleCategories.First().Name,
                 JobType = Category.UserJobCategories.First().Name,
                 IdType = Category.UserIdTypeCategories.First().Name,
            };

            repo.CreateUserAsync(l).Wait();

            var r = repo.GetUserAsync(l.Id).Result;
            Console.WriteLine(r);

            var arr = repo.GetUsersAsync().Result;
            foreach(var user in arr) { Console.WriteLine(user.Id); }
        }
    }
}
