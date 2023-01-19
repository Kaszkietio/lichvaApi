using API.Dtos.Inquiry;
using API.Dtos.Offer;
using API.Dtos.User;
using BankDataLibrary.Entities;
using System.ComponentModel.DataAnnotations;

namespace API.Repositories
{
    public interface IBankRepository
    {
        // Dictionary
        public Task<IEnumerable<Role>> GetRolesAsync();
        public Task<IEnumerable<IdType>> GetIdTypesAsync();
        public Task<IEnumerable<JobType>> GetJobTypesAsync();
        public Task<IEnumerable<OfferStatus>> GetOfferStatusesAsync();

        // Bank
        public Task<IEnumerable<Bank>> GetBanksAsync(
            IList<string>? nameList = null
            );

        // Inquiry
        public Task<IEnumerable<Inquiry>> GetInquiriesAsync(
            User user,
            IList<int>? idFilter = null,
            IList<DateTime>? createDateFilter = null, 
            IList<int>? ammountFilter = null,
            IList<int>? installmentFilter = null,
            IList<int>? bankIdFilter = null
            );
        public Task<OnInquiryCreationDto> CreateInquiryAsync(CreateInquiryDto dto, int userId);
        public Task UpdateInquiryAsync(int id, UpdateInquiryDto dto);

        // Offer
        public Task<IEnumerable<Offer>> GetOffersAsync(
            User user,
            IList<int>? idFilter = null,
            IList<int>? inquiryIdFilter = null,
            IList<DateTime>? createDateFilter = null,
            IList<decimal>? percentageFilter = null,
            IList<decimal>? monthlyInstallmentFilter = null,
            IList<int>? statusFilter = null
            );
        public Task UpdateOfferAsync(int id, UpdateOfferDto dto);


        // User
        public Task<OnUserCreationDto> CreateUserAsync(CreateUserDto dto);
        public Task<IEnumerable<User>> GetUsersAsync(
                    IList<int>? idFilter = null,
                    IList<DateTime>? createDateFilter = null,
                    IList<int>? roleFilter = null,
                    IList<bool>? internalFilter = null,
                    IList<bool>? anonymousFilter = null,
                    IList<string>? emailFilter = null,
                    IList<string>? hashFilter = null
            );
        public Task UpdateUserAsync(int id, UpdateUserDto dto);
        public Task<User> AuthenticateUserAsync(string authToken);

        //////// O L D ////////

        //// Inquiry
        //public Task<IEnumerable<Inquiry>> GetInquiriesAsync(int? inqId = null, int? userId = null);
        //public Task CreateInquiryAsync(Inquiry inquiry);

        //// Offer
        //public Task<Offer?> GetOfferAsync(int userId);
        //public Task<IEnumerable<Offer>> GetOffersAsync(
        //    int? userId = null,
        //    int? inquiryId = null,
        //    int? bankId = null);
        //public Task CreateOfferAsync(Offer offer);
        //public Task UpdateOfferAsync(Offer offer);

        //// OfferHistory
        //public Task<IEnumerable<OfferHistory>> GetOfferHistoryAsync(int? userId = null);
        //public Task CreateOfferHistoryAsync(OfferHistory offer);

        //// User
        //public Task<User?> GetUserAsync(int userId);
        //public Task<User?> GetUserAsync(string email);
        //public Task<IEnumerable<User>> GetUsersAsync();
        //public Task CreateUserAsync(User user);
        //public Task UpdateUserAsync(UpdateUserDto user);
    }
}
