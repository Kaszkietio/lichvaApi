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
            (bool IsRange, IList<int>)? idFilter = null,
            (bool IsRange, IList<DateTime>)? createDateFilter = null, 
            (bool IsRange, IList<int>)? ammountFilter = null,
            (bool IsRange, IList<int>)? installmentFilter = null,
            (bool IsRange, IList<int>)? bankIdFilter = null
            );
        public Task<OnInquiryCreationDto> CreateInquiryAsync(CreateInquiryDto dto, int userId);
        public Task UpdateInquiryAsync(int id, UpdateInquiryDto dto);

        // Offer
        public Task<IEnumerable<Offer>> GetOffersAsync(
            User user,
            (bool IsRange, IList<int>)? idFilter = null,
            (bool IsRange, IList<int>)? inquiryIdFilter = null,
            (bool IsRange, IList<DateTime>)? createDateFilter = null,
            (bool IsRange, IList<decimal>)? percentageFilter = null,
            (bool IsRange, IList<decimal>)? monthlyInstallmentFilter = null,
            (bool IsRange, IList<int>)? statusFilter = null
            );
        public Task UpdateOfferAsync(int id, UpdateOfferDto dto);


        // User
        public Task<OnUserCreationDto> CreateUserAsync(CreateUserDto dto);
        public Task<IEnumerable<User>> GetUsersAsync(
                    (bool IsRange, IList<int>)? idFilter = null,
                    (bool IsRange, IList<DateTime>)? createDateFilter = null,
                    (bool IsRange, IList<int>)? roleFilter = null,
                    (bool IsRange, IList<bool>)? internalFilter = null,
                    (bool IsRange, IList<bool>)? anonymousFilter = null,
                    (bool IsRange, IList<string>)? emailFilter = null,
                    (bool IsRange, IList<string>)? hashFilter = null
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
