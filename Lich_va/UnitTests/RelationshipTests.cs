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

            foreach (var inq in q.Inquiries)
                Console.WriteLine(inq.Id);
        }

        [TestMethod]
        public void InquiryUser()
        {
            using LichvaContext db = new LichvaContext();
            var q = db
                .Inquiries
                .Include(x => x.User)
                .First(inq => inq.UserId != null);

            if (q == null) return;
            Console.WriteLine(q.User.Id);
        }

        [TestMethod]
        public void InquiryOffer()
        {
            using LichvaContext db = new LichvaContext();
            var q = db.Inquiries
                .Include(x => x.Offer)
                .First(x => x.Offer != null);

            if (q == null) return;
            Console.WriteLine(q.Offer.Id);
        }

        [TestMethod]
        public void OfferInquiry()
        {
            using LichvaContext db = new LichvaContext();
            var q = db
                .Offers
                .Include(x => x.Inquiry)
                .First(inq => inq.InquiryId == 1);

            if (q == null) return;
            Console.WriteLine(q.Inquiry.Id);
        }

        [TestMethod]
        public void Inquiry_ForeingInq()
        {
            using LichvaContext db = new LichvaContext();
            var q = db.ForeignInquiries
                .Include(x => x.Inquiry)
                .FirstOrDefault(x => x.Inquiry != null);

            if (q == null) return;
            Console.WriteLine(q.Inquiry.Id);
        }

        [TestMethod]
        public void ForeignInq_Inquiry()
        {
            using LichvaContext db = new LichvaContext();
            var q = db
                .Inquiries
                .Include(x => x.ForeignInquiry)
                .First(inq => inq.ForeignInquiry != null);

            if (q == null) return;
            Console.WriteLine(q.ForeignInquiry.Id);
        }


        [TestMethod]
        public void Bank_ForInq()
        {
            using LichvaContext db = new LichvaContext();
            var q = db.Banks
                .Include(x => x.ForeignInquiries)
                .FirstOrDefaultAsync();

            if (q == null) return;
            q.Wait();

            if (q.Result == null) return;

            foreach (var inq in q.Result.ForeignInquiries)
                Console.WriteLine(inq.Id);
        }

        [TestMethod]
        public void ForInqBank()
        {
            using LichvaContext db = new LichvaContext();
            var q = db
                .ForeignInquiries
                .Include(x => x.Bank)
                .First();

            if (q == null) return;
            Console.WriteLine(q.Bank.Id);
        }

        [TestMethod]
        public void OfferStatus_Offer()
        {
            using LichvaContext db = new LichvaContext();
            var q = db.OfferStatuses
                .Include(x => x.Offers)
                .FirstOrDefaultAsync();

            if (q == null) return;
            q.Wait();

            if (q.Result == null) return;

            foreach (var inq in q.Result.Offers)
                Console.WriteLine(inq.Id);
        }

        [TestMethod]
        public void Offer_OfferStatus()
        {
            using LichvaContext db = new LichvaContext();
            var q = db
                .Offers
                .Include(x => x.OfferStatus)
                .First(inq => inq.OfferStatus != null);

            if (q == null) return;
            Console.WriteLine(q.OfferStatus.Id);
        }

        [TestMethod]
        public void User_OfferHistory()
        {
            using LichvaContext db = new LichvaContext();
            var q = db.Users
                .Include(x => x.OfferHistory)
                .FirstOrDefaultAsync();

            if (q == null) return;
            q.Wait();

            if (q.Result == null) return;

            foreach (var inq in q.Result.OfferHistory)
                Console.WriteLine(inq.Id);
        }

        [TestMethod]
        public void OfferHistory_User()
        {
            using LichvaContext db = new LichvaContext();
            var q = db
                .OfferHistories
                .Include(x => x.Employee)
                .First(inq => inq.EmployeeId != null);

            if (q == null) return;
            Console.WriteLine(q.Employee.Id);
        }

        [TestMethod]
        public void Offer_OfferHistory()
        {
            using LichvaContext db = new LichvaContext();
            var q = db.Offers
                .Include(x => x.History)
                .FirstOrDefaultAsync();

            if (q == null) return;
            q.Wait();

            if (q.Result == null) return;

            foreach (var inq in q.Result.History)
                Console.WriteLine(inq.Id);
        }

        [TestMethod]
        public void OfferHistory_Offer()
        {
            using LichvaContext db = new LichvaContext();
            var q = db
                .OfferHistories
                .Include(x => x.Offer)
                .First(inq => inq.OfferId != null);

            if (q == null) return;
            Console.WriteLine(q.Offer.Id);
        }

        [TestMethod]
        public void User_IdType()
        {
            using LichvaContext db = new LichvaContext();
            var q = db.Users
                .Include(x => x.IdType)
                .First(x => x.IdTypeId != null);

            if (q == null) return;
            Console.WriteLine(q.IdType.Id);
        }

        [TestMethod]
        public void IdType_User()
        {
            using LichvaContext db = new LichvaContext();
            var q = db
                .IdTypes
                .Include(x => x.Users)
                .First(x => x.Users.Count != 0);

            foreach (var inq in q.Users)
                Console.WriteLine(inq.Id);
        }
        [TestMethod]
        public void User_JobType()
        {
            using LichvaContext db = new LichvaContext();
            var q = db.Users
                .Include(x => x.JobType)
                .First(x => x.JobTypeId != null);

            if (q == null) return;
            Console.WriteLine(q.JobType.Id);
        }

        [TestMethod]
        public void JobType_User()
        {
            using LichvaContext db = new LichvaContext();
            var q = db
                .JobTypes
                .Include(x => x.Users)
                .First(x => x.Users.Count != 0);

            foreach (var inq in q.Users)
                Console.WriteLine(inq.Id);
        }
        [TestMethod]
        public void User_Role()
        {
            using LichvaContext db = new LichvaContext();
            var q = db.Users
                .Include(x => x.Role)
                .First(x => x.RoleId != null);

            if (q == null) return;
            Console.WriteLine(q.Role.Id);
        }

        [TestMethod]
        public void Role_User()
        {
            using LichvaContext db = new LichvaContext();
            var q = db
                .Roles
                .Include(x => x.Users)
                .First(x => x.Users.Count != 0);

            foreach (var inq in q.Users)
                Console.WriteLine(inq.Id);
        }
        [TestMethod]
        public void OfferHistory_OfferStatus()
        {
            using LichvaContext db = new LichvaContext();
            var q = db.OfferHistories
                .Include(x => x.Status)
                .First(x => x.StatusId != null);

            if (q == null) return;
            Console.WriteLine(q.Status.Id);
        }

        [TestMethod]
        public void OfferStatus_OfferHistory()
        {
            using LichvaContext db = new LichvaContext();
            var q = db
                .OfferStatuses
                .Include(x => x.OfferHistories)
                .First(x => x.OfferHistories.Count != 0);

            foreach (var inq in q.OfferHistories)
                Console.WriteLine(inq.Id);
        }


    }
}
