using BankDataLibrary.Config;
using BankDataLibrary.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class DBBankRepository : IBankRepository
    {
        public void ChangeOfferStatus(int offerID, Offer.Status status)
        {
            throw new NotImplementedException();
        }

        public void ChangleRole(int userID, User.Role role)
        {
            throw new NotImplementedException();
        }

        public bool CheckForAdmin(string APIToken)
        {
            throw new NotImplementedException();
        }

        public bool CheckForBank(string APIToken)
        {
            throw new NotImplementedException();
        }

        public bool CheckForEmployee(string APIToken)
        {
            throw new NotImplementedException();
        }

        public void CheckForPlatformPermission(string APIToken)
        {
            throw new NotImplementedException();
        }

        public bool CheckForUser(string APIToken)
        {
            throw new NotImplementedException();
        }

        public void CheckIfUserExists(string email)
        {
            throw new NotImplementedException();
        }

        public bool CheckIfUserRegisterable(string APIToken)
        {
            throw new NotImplementedException();
        }

        public string CreateDoc(int offerID)
        {
            throw new NotImplementedException();
        }

        public User CreateExternalUser(UserData userData)
        {
            throw new NotImplementedException();
        }

        public Offer CreateInitialOffer(int userID, int installments, int ammount)
        {
            throw new NotImplementedException();
        }

        public void CreateInquiry(Inquiry inquiry)
        {
            using LichvaContext db = new();
            db.Inquiries.Add(inquiry);
            db.SaveChanges();
        }

        public void CreateOffer(Offer offer)
        {
            throw new NotImplementedException();
        }

        public User CreateUser(string email, string GID)
        {
            throw new NotImplementedException();
        }

        public List<Offer> GetAllInitialOffers(int userID, int installments, int ammount)
        {
            throw new NotImplementedException();
        }

        public List<Offer> GetExtendedOffers(int offset, string[] sorts, string filter)
        {
            throw new NotImplementedException();
        }

        public List<User> GetExtendedUsers(int offset, string[] sorts, string filter)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Inquiry> GetInquires()
        {
            using LichvaContext db = new();
            return db.Inquiries.ToList();
        }

        public Inquiry? GetInquiry(int id)
        {
            IEnumerable<Inquiry> inquiries = GetInquires();
            Inquiry? inquiry = inquiries.FirstOrDefault(x => x.id == id);
            return inquiry;
        }

        public Offer? GetOffer(int offerId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Offer> GetOffers()
        {
            throw new NotImplementedException();
        }

        public List<Offer> GetOffers(string callAPIToken, int offset, string[] sorts, string filter)
        {
            throw new NotImplementedException();
        }

        public Offer.Status GetOfferStatus(int offerID)
        {
            throw new NotImplementedException();
        }

        public string GetUserAPIToken(string email)
        {
            throw new NotImplementedException();
        }

        public User GetUserData(string APIToken)
        {
            throw new NotImplementedException();
        }

        public int GetUserID(string mail)
        {
            throw new NotImplementedException();
        }

        public UserPanelData GetUserPanelData(string APIToken)
        {
            throw new NotImplementedException();
        }

        public void LoginUser(string APIToken)
        {
            throw new NotImplementedException();
        }

        public User RegisterUser(string APIToken, string firstName, string lastName, User.JobCategories jobType, double incomeLevel, User.IdTypes idType, string idNumber)
        {
            throw new NotImplementedException();
        }

        public bool SearchForUser(string mail)
        {
            throw new NotImplementedException();
        }

        public void SendUpdateEmail(int offerID)
        {
            throw new NotImplementedException();
        }

        public void UploadDoc(int offerID, byte[] signedDoc)
        {
            throw new NotImplementedException();
        }
    }
}
