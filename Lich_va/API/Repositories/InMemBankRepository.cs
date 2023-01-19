using API.Dtos.Inquiry;
using API.Dtos.Offer;
using API.Dtos.User;
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

        public Task<User> AuthenticateUserAsync(string authToken)
        {
            throw new NotImplementedException();
        }

        public Task<OnInquiryCreationDto> CreateInquiry(CreateInquiryDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<OnInquiryCreationDto> CreateInquiry(CreateInquiryDto dto, int userId)
        {
            throw new NotImplementedException();
        }

        public Task CreateInquiryAsync(Inquiry inquiry)
        {
            throw new NotImplementedException();
        }

        public Task<OnInquiryCreationDto> CreateInquiryAsync(CreateInquiryDto dto, int userId)
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

        public Task<OnUserCreationDto> CreateUser(GetUserDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<OnUserCreationDto> CreateUser(CreateUserDto dto)
        {
            throw new NotImplementedException();
        }

        public Task CreateUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<OnUserCreationDto> CreateUserAsync(CreateUserDto dto)
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

        public Task<IEnumerable<Bank>> GetBanksAsync(IList<string>? nameList = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IdType>> GetIdTypesAsync()
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

        public Task<IEnumerable<Inquiry>> GetInquiriesAsync(IList<int>? idFilter = null, IList<DateTime>? createDateFilter = null, IList<int>? ammountFilter = null, IList<int>? installmentFilter = null, IList<int>? bankIdFilter = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Inquiry>> GetInquiriesAsync(User user, IList<int>? idFilter = null, IList<DateTime>? createDateFilter = null, IList<int>? ammountFilter = null, IList<int>? installmentFilter = null, IList<int>? bankIdFilter = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Inquiry>> GetInquiriesAsync(User user, (bool IsRange, IList<int>)? idFilter = null, (bool IsRange, IList<DateTime>)? createDateFilter = null, (bool IsRange, IList<int>)? ammountFilter = null, (bool IsRange, IList<int>)? installmentFilter = null, (bool IsRange, IList<int>)? bankIdFilter = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<JobType>> GetJobTypesAsync()
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

        public Task<IEnumerable<Offer>> GetOffersAsync(IList<int>? idFilter, IList<int>? inquiryIdFiler, IList<DateTime>? createDateFilter, IList<decimal>? percentageFilter, IList<decimal>? monthlyInstallmentFilter, IList<int>? statusFilter)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Offer>> GetOffersAsync(User user, IList<int>? idFilter = null, IList<int>? inquiryIdFilter = null, IList<DateTime>? createDateFilter = null, IList<decimal>? percentageFilter = null, IList<decimal>? monthlyInstallmentFilter = null, IList<int>? statusFilter = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Offer>> GetOffersAsync(User user, (bool IsRange, IList<int>)? idFilter = null, (bool IsRange, IList<int>)? inquiryIdFilter = null, (bool IsRange, IList<DateTime>)? createDateFilter = null, (bool IsRange, IList<decimal>)? percentageFilter = null, (bool IsRange, IList<decimal>)? monthlyInstallmentFilter = null, (bool IsRange, IList<int>)? statusFilter = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OfferStatus>> GetOfferStatusesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Role>> GetRolesAsync()
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

        public Task<IEnumerable<User>> GetUsers(IList<int>? idFilter = null, IList<DateTime>? createDateFilter = null, IList<int>? roleFilter = null, IList<bool>? internalFilter = null, IList<bool>? anonymousFilter = null, IList<string>? emailFilter = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetUsersAsync(IList<int>? idFilter = null, IList<DateTime>? createDateFilter = null, IList<int>? roleFilter = null, IList<bool>? internalFilter = null, IList<bool>? anonymousFilter = null, IList<string>? emailFilter = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetUsersAsync(IList<int> idFilter, IList<DateTime> createDateFilter, IList<string> emailFilter, IList<int> roleFilter, IList<bool> internalFilter, IList<bool> anonymousFilter)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetUsersAsync(IList<int>? idFilter = null, IList<DateTime>? createDateFilter = null, IList<int>? roleFilter = null, IList<bool>? internalFilter = null, IList<bool>? anonymousFilter = null, IList<string>? emailFilter = null, IList<string>? hashFilter = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetUsersAsync((bool IsRange, IList<int>)? idFilter = null, (bool IsRange, IList<DateTime>)? createDateFilter = null, (bool IsRange, IList<int>)? roleFilter = null, (bool IsRange, IList<bool>)? internalFilter = null, (bool IsRange, IList<bool>)? anonymousFilter = null, (bool IsRange, IList<string>)? emailFilter = null, (bool IsRange, IList<string>)? hashFilter = null)
        {
            throw new NotImplementedException();
        }

        public Task UpdateInquiry(UpdateInquiryDto dto)
        {
            throw new NotImplementedException();
        }

        public Task UpdateInquiry(int id, UpdateInquiryDto dto)
        {
            throw new NotImplementedException();
        }

        public Task UpdateInquiryAsync(int id, UpdateInquiryDto dto)
        {
            throw new NotImplementedException();
        }

        public Task UpdateOffer(int id, UpdateOfferDto dto)
        {
            throw new NotImplementedException();
        }

        public Task UpdateOfferAsync(Offer offer)
        {
            throw new NotImplementedException();
        }

        public Task UpdateOfferAsync(int id, UpdateOfferDto dto)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUser(int id, UpdateUserDto dto)
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

        public Task UpdateUserAsync(int id, UpdateUserDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
