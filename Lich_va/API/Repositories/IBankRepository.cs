using BankDataLibrary.Entities;
using System.ComponentModel.DataAnnotations;

namespace API.Repositories
{
    public interface IBankRepository
    {
        // Bank
        public Task<Bank?> GetBankAsync(int bankId);
        public Task<IEnumerable<Bank>> GetBanksAsync();

        // Inquiry
        public Task<IEnumerable<Inquiry>> GetInquiriesAsync(int? inqId = null, int? userId = null);
        public Task CreateInquiryAsync(Inquiry inquiry);

        // Login
        public Task<LoginHistory?> GetLoginHistoryAsync(int userId);
        public Task<IEnumerable<LoginHistory>> GetLoginHistoriesAsync();
        public Task CreateLoginHistoryAsync(LoginHistory login);

        // Offer
        public Task<Offer?> GetOfferAsync(int userId);
        public Task<IEnumerable<Offer>> GetOffersAsync(
            int? userId = null,
            int? inquiryId = null,
            int? bankId = null);
        public Task CreateOfferAsync(Offer offer);
        public Task UpdateOfferAsync(Offer offer);

        // OfferHistory
        public Task<IEnumerable<OfferHistory>> GetOfferHistoryAsync(int? userId = null);
        public Task CreateOfferHistoryAsync(OfferHistory offer);

        // User
        public Task<User?> GetUserAsync(int userId);
        public Task<IEnumerable<User>> GetUsersAsync();
        public Task CreateUserAsync(User user);
        public Task UpdateUserAsync(User user);
    }
}
