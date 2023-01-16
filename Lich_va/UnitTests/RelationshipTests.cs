using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class RelationshipTests
    {
        [TestMethod]
        public void UserInquiry()
        {
            using LichvaContext db = new LichvaContext();
            var q = db.Users
                .Include(user => user.Inquiries)
                .FirstOrDefault(user => user.Id == 1);
            if (q == null) return;

            foreach(var inq in q.Inquiries)
                Console.WriteLine(inq.Id);
        }

        [TestMethod]
        public void InquiryUser()
        {
            using LichvaContext db = new LichvaContext();
            var q = db
                .Inquiries
                .Include(x => x.User)
                .First(inq => inq.UserId == 1);
            
            if (q == null) return;
            Console.WriteLine(q.User.Id);
        }

        [TestMethod]
        public void UserOffer()
        {
            using LichvaContext db = new LichvaContext();
            var q = db.Users
                .Include(x => x.Offers)
                .FirstOrDefaultAsync(x => x.Id == 1);

            if (q == null) return;
            q.Wait();
            
            if(q.Result == null) return;

            foreach(var inq in q.Result.Offers)
                Console.WriteLine(inq.Id);
        }

        [TestMethod]
        public void OfferUser()
        {
            using LichvaContext db = new LichvaContext();
            var q = db
                .Offers
                .Include(x => x.User)
                .First(inq => inq.UserId == 1);
            
            if (q == null) return;
            Console.WriteLine(q.User.Id);
        }

        [TestMethod]
        public void BankOffer()
        {
            using LichvaContext db = new LichvaContext();
            var q = db.Banks
                .Include(x => x.Offers)
                .FirstOrDefaultAsync(x => x.Id == 1);

            if (q == null) return;
            q.Wait();
            
            if(q.Result == null) return;

            foreach(var inq in q.Result.Offers)
                Console.WriteLine(inq.Id);
        }

        [TestMethod]
        public void OfferBank()
        {
            using LichvaContext db = new LichvaContext();
            var q = db
                .Offers
                .Include(x => x.Bank)
                .First(inq => inq.BankId == 1);
            
            if (q == null) return;
            Console.WriteLine(q.Bank.Id);
        }

        [TestMethod]
        public void PlatfomOffer()
        {
            using LichvaContext db = new LichvaContext();
            var q = db.Banks
                .Include(x => x.PlatformOffers)
                .FirstOrDefaultAsync(x => x.Id == 2);

            if (q == null) return;
            q.Wait();
            
            if(q.Result == null) return;

            foreach(var inq in q.Result.PlatformOffers)
                Console.WriteLine(inq.Id);
        }

        [TestMethod]
        public void OfferPlatform()
        {
            using LichvaContext db = new LichvaContext();
            var q = db
                .Offers
                .Include(x => x.Platform)
                .First(inq => inq.PlatformId == 2);
            
            if (q == null) return;
            Console.WriteLine(q.Platform.Id);
        }

        [TestMethod]
        public void User_OfferHistory()
        {
            using LichvaContext db = new LichvaContext();
            var q = db.Users
                .Include(x => x.OfferHistory)
                .FirstOrDefaultAsync(x => x.Id == 1);

            if (q == null) return;
            q.Wait();
            
            if(q.Result == null) return;

            foreach(var inq in q.Result.OfferHistory)
                Console.WriteLine(inq.Id);
        }

        [TestMethod]
        public void OfferHistory_User()
        {
            using LichvaContext db = new LichvaContext();
            var q = db
                .OfferHistories
                .Include(x => x.Employee)
                .First(inq => inq.EmployeeId == 1);
            
            if (q == null) return;
            Console.WriteLine(q.Employee.Id);
        }

        [TestMethod]
        public void Offer_OfferHistory()
        {
            using LichvaContext db = new LichvaContext();
            var q = db.Offers
                .Include(x => x.History)
                .FirstOrDefaultAsync(x => x.Id == 1);

            if (q == null) return;
            q.Wait();
            
            if(q.Result == null) return;

            foreach(var inq in q.Result.History)
                Console.WriteLine(inq.Id);
        }

        [TestMethod]
        public void OfferHistory_Offer()
        {
            using LichvaContext db = new LichvaContext();
            var q = db
                .OfferHistories
                .Include(x => x.Offer)
                .First(inq => inq.OfferId == 1);
            
            if (q == null) return;
            Console.WriteLine(q.Offer.Id);
        }

        [TestMethod]
        public void User_LoginHistory()
        {
            using LichvaContext db = new LichvaContext();
            var q = db.Users
                .Include(x => x.Logins)
                .FirstOrDefaultAsync(x => x.Id == 1);

            if (q == null) return;
            q.Wait();
            
            if(q.Result == null) return;

            foreach(var inq in q.Result.Logins)
                Console.WriteLine(inq.Id);
        }

        [TestMethod]
        public void LoginHistory_User()
        {
            using LichvaContext db = new LichvaContext();
            var q = db
                .LoginHistories
                .Include(x => x.User)
                .First(inq => inq.UserId == 1);
            
            if (q == null) return;
            Console.WriteLine(q.User.Id);
        }
    }
}
