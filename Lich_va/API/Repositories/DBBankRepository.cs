using API.Dtos.Inquiry;
using API.Dtos.Offer;
using API.Dtos.User;
using API.Entities;
using API.Helpers;
using BankDataLibrary.Config;
using BankDataLibrary.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.IdentityModel.Tokens;
using static API.Repositories.IBankRepository;

namespace API.Repositories
{
    public class DBBankRepository : IBankRepository
    {
        // New
        // Dictionary
        public async Task<IEnumerable<Role>> GetRolesAsync()
        {
            using LichvaContext db = new();
            return await db.Roles.ToListAsync();
        }

        public async Task<IEnumerable<IdType>> GetIdTypesAsync()
        {
            using LichvaContext db = new();
            return await db.IdTypes.ToListAsync();
        }

        public async Task<IEnumerable<JobType>> GetJobTypesAsync()
        {
            using LichvaContext db = new();
            return await db.JobTypes.ToListAsync();
        }

        public async Task<IEnumerable<OfferStatus>> GetOfferStatusesAsync()
        {
            using LichvaContext db = new();
            return await db.OfferStatuses.ToListAsync();
        }


        // Bank
        public async Task<IEnumerable<Bank>> GetBanksAsync(
            IList<string>? nameList = null
            )
        {
            nameList ??= new List<string>();
            return await InternalGetBanksAsync(nameList);
        }

        public async Task<IEnumerable<Bank>> InternalGetBanksAsync(IList<string> nameList)
        {
            using LichvaContext db = new();
            var result = db.Banks.AsQueryable();
            if (nameList.Count != 0)
                result = result.Where(bank => 
                    bank.Name != null && 
                    nameList.Contains(bank.Name)
                    );

            return await result.ToListAsync();
        }


        // Inquiry
        public async Task<IEnumerable<Inquiry>> GetInquiriesAsync(
            User user,
            IList<int>? idFilter = null,
            IList<DateTime>? createDateFilter = null,
            IList<int>? ammountFilter = null,
            IList<int>? installmentFilter = null,
            IList<int>? bankIdFilter = null
            )
        {
            idFilter??= new List<int>();
            createDateFilter ??= new List<DateTime>();
            ammountFilter ??= new List<int>();
            installmentFilter ??= new List<int>();
            bankIdFilter ??= new List<int>();
            return await InternalGetInquiriesAsync(user, idFilter, createDateFilter, ammountFilter, installmentFilter, bankIdFilter);
        }

        public async Task<IEnumerable<Inquiry>> InternalGetInquiriesAsync(
            User user,
            IList<int> idFilter,
            IList<DateTime> createDateFilter,
            IList<int> ammountFilter,
            IList<int> installmentFilter,
            IList<int> bankIdFilter
            )
        {
            using LichvaContext db = new();
            var result = db.Inquiries
                .Include(x => x.Offer)
                .ThenInclude(x => x.History)
                .Include(x => x.Offer)
                .ThenInclude(x => x.OfferStatus)
                .Where(x => x.UserId == user.Id);


            if (idFilter.Count != 0)
                result = result.Where(inq =>
                    idFilter.Contains(inq.Id)
                );
            if (createDateFilter.Count != 0)
                result = result.Where(inq =>
                    inq.CreationDate.HasValue && 
                    createDateFilter.Contains(inq.CreationDate.Value)
                );
            if (ammountFilter.Count != 0)
                result = result.Where(inq =>
                    inq.Ammount.HasValue &&
                    ammountFilter.Contains(inq.Ammount.Value)
                );
            if (installmentFilter.Count != 0)
                result = result.Where(inq =>
                    inq.Installments.HasValue && 
                    installmentFilter.Contains(inq.Installments.Value)
                );
            if (bankIdFilter.Count != 0)
            {
                var lichvaBank =  await db.Banks.FirstOrDefaultAsync(bank => bank.Name == "Lichva");
                int lichvaBankId = lichvaBank?.Id ?? 0;
                result.Include(x => x.ForeignInquiry);
                result = result.Where(inq =>
                    (inq.ForeignInquiry == null && bankIdFilter.Contains(lichvaBankId)) || 
                    (inq.ForeignInquiry != null && bankIdFilter.Contains(inq.ForeignInquiry.BankId))
                );
            }

            return await result.ToListAsync();
        }

        public async Task<OnInquiryCreationDto> CreateInquiryAsync(CreateInquiryDto dto, int userId)
        {
            using LichvaContext db = new();

            Inquiry inquiry = new Inquiry
            {
                 CreationDate = DateTime.Now,
                 UserId = userId,
                 Ammount = dto.Value,
                 Installments = dto.InstallmentsNumber,
            };

            await db.Inquiries.AddAsync(inquiry);
            await db.SaveChangesAsync();

            // TODO:
            Task createOfferTast = Task.Factory.StartNew(() => CreateOfferTask(inquiry.Id));
            //createOfferTast.Start();
            

            return db.Inquiries
                .Include(x => x.Offer)
                .ThenInclude(x => x.OfferStatus)
                .Include(x => x.Offer)
                .ThenInclude(x => x.History)
                .First(x => x.Id == inquiry.Id)
                .AsOnCreationDto();
        }

        public async Task UpdateInquiryAsync(int id, UpdateInquiryDto dto)
        {
            using LichvaContext db = new();
            Inquiry inquiry = await db.Inquiries.FirstAsync(x => x.Id == id);

            inquiry.Ammount = dto.Value;
            inquiry.Installments = dto.InstallmentsNumber;

            await db.SaveChangesAsync();
        }


        // Offer
        public async Task<IEnumerable<Offer>> GetOffersAsync(
            User user,
            IList<int>? idFilter = null,
            IList<int>? inquiryIdFilter = null,
            IList<DateTime>? createDateFilter = null,
            IList<decimal>? percentageFilter = null,
            IList<decimal>? monthlyInstallmentFilter = null,
            IList<int>? statusFilter = null 
            )
        {
            idFilter??= new List<int>();
            inquiryIdFilter ??= new List<int>();
            createDateFilter??= new List<DateTime>();
            percentageFilter ??= new List<decimal>();
            monthlyInstallmentFilter ??= new List<decimal>();
            statusFilter ??= new List<int>();

            return await InternalGetOffersAsync(user, idFilter, inquiryIdFilter, createDateFilter, percentageFilter, monthlyInstallmentFilter, statusFilter);
        }

        public async Task<IEnumerable<Offer>> InternalGetOffersAsync(
            User user,
            IList<int> idFilter,
            IList<int> inquiryIdFilter,
            IList<DateTime> createDateFilter,
            IList<decimal> percentageFilter,
            IList<decimal> monthlyInstallmentFilter,
            IList<int> statusFilter 
            )
        {
            using LichvaContext db = new();
            var result = db.Offers
                .Include(x => x.Inquiry)
                .Include(x => x.History)
                .Include(x => x.OfferStatus)
                .Where(x => x.Inquiry.UserId == user.Id);

            if(idFilter.Count != 0)
                result = result.Where(x => idFilter.Contains(x.Id));
            if(inquiryIdFilter.Count != 0)
                result = result.Where(x => x.InquiryId.HasValue && inquiryIdFilter.Contains(x.InquiryId.Value));
            if(createDateFilter.Count != 0)
                result = result.Where(x => x.CreationDate.HasValue && createDateFilter.Contains(x.CreationDate.Value));
            if(percentageFilter.Count != 0)
                result = result.Where(x => x.Percentage.HasValue && percentageFilter.Contains(x.Percentage.Value));
            if(monthlyInstallmentFilter.Count != 0)
                result = result.Where(x => x.MonthlyInstallment.HasValue && monthlyInstallmentFilter.Contains(x.MonthlyInstallment.Value));
            if(statusFilter.Count != 0)
                result = result.Where(x => x.StatusId.HasValue && statusFilter.Contains(x.StatusId.Value));

            return await result.ToListAsync();
        }

        public void CreateOfferTask(int inqId)
        {
            Random r = new Random();
            int sleepTimeMili = r.Next(1 * 1000, 10 * 1000);
            Thread.Sleep(sleepTimeMili);

            using LichvaContext db = new();
            Offer offer = new Offer()
            {
                CreationDate = DateTime.Now,
                InquiryId = inqId,
                Percentage = (decimal?)r.NextDouble(),
                MonthlyInstallment = (decimal?)r.NextDouble(),
                StatusId = db.OfferStatuses.First(x => x.Name == "waiting_for_acceptance").Id,
                DocumentLink = null,
            };
            db.Offers.Add(offer);
            db.SaveChanges();
        }

        public async Task UpdateOfferAsync(int id, UpdateOfferDto dto)
        {
            using LichvaContext db = new();
            Offer offer = await db.Offers.FirstAsync(x => x.Id == id);

            offer.StatusId = dto.Status;
            offer.DocumentLink = dto.DocumentLink;

            await db.SaveChangesAsync();
        }

        // User
        public async Task<OnUserCreationDto> CreateUserAsync(CreateUserDto dto)
        {
            using LichvaContext db = new();

            bool isPropValid = false;

            if(dto.Active.HasValue && dto.Active.Value)
            {
                // Check if valid jobTypeId
                isPropValid = await db.JobTypes.AnyAsync(x => x.Id == dto.JobTypeId);
                if (!isPropValid)
                {
                    throw new InvalidDataException("Invalid job type id");
                }

                // Check if valid IdTypeId
                isPropValid = await db.IdTypes.AnyAsync(x => x.Id == dto.IdTypeId);
                if (!isPropValid)
                {
                    throw new InvalidDataException("Invalid id type id");
                }

                // Check if valid IdTypeId
                isPropValid = await db.Roles.AnyAsync(x => x.Id == dto.RoleId);
                if (!isPropValid)
                {
                    throw new InvalidDataException("Invalid role id");
                }
            }

            User user = new User
            {
                CreationDate = DateTime.Now,
                RoleId = dto.RoleId,
                Hash = dto.Email,
                Internal = true,
                Anonymous= dto.Anonymous,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                JobTypeId = dto.JobTypeId,
                IncomeLevel = dto.IncomeLevel,
                IdTypeId = dto.IdTypeId,
                IdNumber = dto.IdNumber,
            };
            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();

            return user.AsOnCreationDto();
        }
        public async Task<IEnumerable<User>> GetUsersAsync(
            IList<int>? idFilter = null, 
            IList<DateTime>? createDateFilter = null, 
            IList<int>? roleFilter = null, 
            IList<bool>? internalFilter = null, 
            IList<bool>? anonymousFilter = null, 
            IList<string>? emailFilter = null,
            IList<string>? hashFilter = null
            )
        {
            idFilter ??= new List<int>();
            createDateFilter ??= new List<DateTime>();
            roleFilter ??= new List<int>();
            internalFilter ??= new List<bool>();
            anonymousFilter ??= new List<bool>();
            emailFilter ??= new List<string>();
            hashFilter ??= new List<string>();

            return await InternalGetUsersAsync(idFilter, createDateFilter, emailFilter, roleFilter, internalFilter, anonymousFilter, hashFilter);
        }


        public async Task<IEnumerable<User>> InternalGetUsersAsync(
            IList<int> idFilter,
            IList<DateTime> createDateFilter,
            IList<string> emailFilter,
            IList<int> roleFilter,
            IList<bool> internalFilter,
            IList<bool> anonymousFilter,
            IList<string> hashFilter
            )
        {
            using LichvaContext db = new();
            var result = db.Users.AsQueryable();

            if (idFilter.Count != 0)
                result = result.Where(x => idFilter.Contains(x.Id));
            if (createDateFilter.Count != 0)
                result = result.Where(x =>
                    x.CreationDate.HasValue && createDateFilter.Contains(x.CreationDate.Value)
                    );
            if (roleFilter.Count != 0)
                result = result.Where(x =>
                    x.RoleId.HasValue && roleFilter.Contains(x.RoleId.Value)
                    );
            if (internalFilter.Count != 0)
                result = result.Where(x =>
                    x.Internal.HasValue && internalFilter.Contains(x.Internal.Value)
                    );
            if (anonymousFilter.Count != 0)
                result = result.Where(x =>
                    x.Anonymous.HasValue && anonymousFilter.Contains(x.Anonymous.Value)
                    );
            if (emailFilter.Count != 0)
                result = result.Where(x =>
                    x.Email != null && emailFilter.Contains(x.Email)
                    );
            if (hashFilter.Count != 0)
                result = result.Where(x =>
                    x.Hash != null && hashFilter.Contains(x.Hash)
                    );

            var tmp = await result.ToListAsync();

            return tmp;
        }


        public async Task UpdateUserAsync(int id, UpdateUserDto dto)
        {
            using LichvaContext db = new();

            // Check if user exists
            User? user = await db.Users.FirstOrDefaultAsync(x => x.Id == id); 
            if(user == null)
            {
                throw new InvalidDataException("Invalid user id");
            }

            // Check if role, id type and job type ids are valid
            bool isPropValid = false;
            // Check if valid jobTypeId
            isPropValid = await db.JobTypes.AnyAsync(x => x.Id == dto.JobTypeId);
            if(!isPropValid)
            {
                throw new InvalidDataException("Invalid job type id");
            }

            // Check if valid IdTypeId
            isPropValid = await db.IdTypes.AnyAsync(x => x.Id == dto.IdTypeId);
            if(!isPropValid)
            {
                throw new InvalidDataException("Invalid id type id");
            }

            // Check if valid IdTypeId
            isPropValid = await db.Roles.AnyAsync(x => x.Id == dto.RoleId);
            if(!isPropValid)
            {
                throw new InvalidDataException("Invalid role id");
            }

            db.Entry(user).CurrentValues.SetValues(dto);
            await db.SaveChangesAsync();
        }

        public async Task<User> AuthenticateUserAsync(string authToken)
        {
            using LichvaContext db = new();
            string userHash = Security.Decrypt(AppSettings.Instance.HashKey, authToken);

            User? user = await db.Users.FirstOrDefaultAsync(x => x.Hash == userHash);
            if(user == null)
            {
                throw new InvalidDataException("Invalid authentication token");
            }

            return user;
        }

        // OLD
        //public async Task CreateInquiryAsync(Inquiry inquiry)
        //{
        //    using LichvaContext db = new LichvaContext();
        //    await db.AddAsync(inquiry);
        //    await db.SaveChangesAsync();
        //}

        //public async Task CreateOfferAsync(Offer offer)
        //{
        //    using LichvaContext db = new LichvaContext();
        //    await db.AddAsync(offer);
        //    await db.SaveChangesAsync();
        //}

        //public async Task CreateOfferHistoryAsync(OfferHistory offer)
        //{
        //    using LichvaContext db = new LichvaContext();
        //    await db.AddAsync(offer);
        //    await db.SaveChangesAsync();
        //}

        //public async Task CreateUserAsync(User user)
        //{
        //    using LichvaContext db = new LichvaContext();
        //    await db.AddAsync(user);
        //    await db.SaveChangesAsync();
        //}

        //public async Task<Bank?> GetBankAsync(int bankId)
        //{
        //    using LichvaContext db = new LichvaContext();
        //    return await db.Banks
        //        .FirstOrDefaultAsync(bank => bank.Id == bankId);
        //}

        //public async Task<IEnumerable<Bank>> GetBanksAsync()
        //{
        //    using LichvaContext db = new LichvaContext();
        //    return await db.Banks
        //        .ToListAsync();
        //}

        //public async Task<IEnumerable<Inquiry>> GetInquiriesAsync(int? inqId = null, int? userId = null)
        //{
        //    using LichvaContext db = new LichvaContext();
        //    return await db.Inquiries
        //        .Where(inq => inqId == null || inqId == inq.Id)
        //        .Where(inq => userId == null || userId == inq.UserId)
        //        .ToListAsync();
        //}

        //public async Task<Offer?> GetOfferAsync(int userId)
        //{
        //    using LichvaContext db = new LichvaContext();
        //    return await db.Offers.Include(x => x.Inquiry).FirstOrDefaultAsync(offer => offer.Inquiry != null && offer.Inquiry.UserId == userId);
        //}

        //public async Task<IEnumerable<OfferHistory>> GetOfferHistoryAsync(int? userId = null)
        //{
        //    using LichvaContext db = new LichvaContext();
        //    return await db.OfferHistories.ToListAsync();
        //}

        //public async Task<IEnumerable<Offer>> GetOffersAsync(int? userId = null, int? inquiryId = null, int? bankId = null)
        //{
        //    using LichvaContext db = new LichvaContext();
        //    IQueryable<Offer> filter = db.Offers.Include(offer => offer.Inquiry);
        //    filter = filter.Include(x => x.Inquiry.ForeignInquiry);

        //    if (userId != null)
        //        filter = filter.Where(offer => offer.Inquiry != null && offer.Inquiry.UserId == userId);
        //    if (inquiryId != null)
        //        filter = filter.Where(offer => offer.Id == inquiryId);
        //    if (bankId != null)
        //        filter = filter.Where(offer => offer.Inquiry.ForeignInquiry.BankId == bankId);

        //    return await filter.ToListAsync();
        //}

        //public async Task<User?> GetUserAsync(int userId)
        //{
        //    using LichvaContext db = new LichvaContext();
        //    return await db.Users.FirstOrDefaultAsync(x => x.Id == userId);
        //}

        //public async Task<User?> GetUserAsync(string email)
        //{
        //    using LichvaContext db = new LichvaContext();
        //    return await db.Users.FirstOrDefaultAsync(x => x.Email == email);
        //}

        //public async Task<IEnumerable<User>> GetUsersAsync()
        //{
        //    using LichvaContext db = new LichvaContext();
        //    return await db.Users.ToListAsync();
        //}

        //public async Task UpdateOfferAsync(Offer offer)
        //{
        //    using LichvaContext db = new LichvaContext();
        //    Offer? current = await db.Offers.FirstOrDefaultAsync(x => x.Id == offer.Id);
        //    if (current == null) return;

        //    db.Entry(current).CurrentValues.SetValues(offer);

        //    await db.SaveChangesAsync();
        //}

        //public async Task UpdateUserAsync(UpdateUserDto user)
        //{
        //    using LichvaContext db = new LichvaContext();
        //    User? current = await db.Users.FirstOrDefaultAsync(x => x.Email == user.Email);
        //    if (current == null) return;

        //    db.Entry(current).CurrentValues.SetValues(user);
        //    current.Internal = user.Active;

        //    await db.SaveChangesAsync();
        //}
    }
}

