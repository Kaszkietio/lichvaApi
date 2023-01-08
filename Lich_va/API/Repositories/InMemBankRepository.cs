using BankDataLibrary.Entities;

namespace API.Repositories
{
    public class InMemBankRepository : IBankRepository
    {
        List<Inquire> Inquires { get; } = new List<Inquire>();
        List<Offer> Offers { get; } = new List<Offer>();

        public void CreateInquire(Inquire inquire)
        {
            Inquires.Add(inquire);
        }

        public Inquire? GetInquire(int id)
        {
            return Inquires.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Inquire> GetInquires()
        {
            return Inquires;
        }

        public Offer? GetOffer(int offerId)
        {
            return Offers.FirstOrDefault(x => x.Id == offerId);
        }

        public IEnumerable<Offer> GetOffers()
        {
            return Offers;
        }

        public void CreateOffer(Offer offer)
        {
            Offers.Add(offer);
        }

        public void ChangeOfferStatus(int offerID, Offer.Status status)
        {
        }

        public void ChangleRole(int userID, User.Role role)
        {
        }

        public bool CheckForAdmin(string APIToken)
        {
            return true;
        }

        public bool CheckForBank(string APIToken)
        {
            return false;
        }

        public bool CheckForEmployee(string APIToken)
        {
            try
            {
                return true;
            }
            catch(Exception ex)
            {
                throw new InvalidDataException("There is no such employee.", ex);
            }
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
