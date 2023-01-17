using API.Dtos;
using BankDataLibrary.Entities;

namespace API.Repositories
{
    public class InMemBankRepository : IBankRepository
    {
        List<Inquiry> Inquires { get; } = new List<Inquiry>() 
        { 
            //new Inquiry 
            //{
            //    id = 1,
                 
            //} 
        };
        List<Offer> Offers { get; } = new List<Offer>()
        {
            new Offer
            {
                 Id = 1,
                 //status = Offer.OfferStatus.Offered,
            },
        };

        public Task CreateInquiryAsync(Inquiry inquiry)
        {
            throw new NotImplementedException();
        }

        public Task CreateLoginHistoryAsync(LoginHistory login)
        {
            throw new NotImplementedException();
        }

        public Task CreateOfferAsync(Offer offer)
        {
            throw new NotImplementedException();
        }

        public Task CreateOfferHistoryAsync(OfferHistory offer)
        {
            throw new NotImplementedException();
        }

        public Task CreateUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<Bank?> GetBankAsync(int bankId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Bank>> GetBanksAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Inquiry>> GetInquiriesAsync(int? userId = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Inquiry>> GetInquiriesAsync(int? inqId = null, int? userId = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LoginHistory>> GetLoginHistoriesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<LoginHistory?> GetLoginHistoryAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<Offer?> GetOfferAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OfferHistory>> GetOfferHistoryAsync(int? userId = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Offer>> GetOffersAsync(int? userId = null, int? inquiryId = null, int? bankId = null)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetUserAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetUserAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateOfferAsync(Offer offer)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserAsync(UpdateUserDto user)
        {
            throw new NotImplementedException();
        }
    }
}
