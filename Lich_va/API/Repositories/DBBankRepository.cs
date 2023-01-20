using API.Dtos.Inquiry;
using API.Dtos.Offer;
using API.Dtos.User;
using API.Entities;
using API.Helpers;
using BankDataLibrary.Config;
using BankDataLibrary.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
//using Microsoft.Extensions.Azure;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
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
            (bool IsRange, IList<int>)? idFilter = null,
            (bool IsRange, IList<DateTime>)? createDateFilter = null,
            (bool IsRange, IList<int>)? ammountFilter = null,
            (bool IsRange, IList<int>)? installmentFilter = null,
            (bool IsRange, IList<int>)? bankIdFilter = null
            )
        {
            idFilter??= (false, new List<int>());
            createDateFilter ??= (false, new List<DateTime>());
            ammountFilter ??= (false, new List<int>());
            installmentFilter ??= (false, new List<int>());
            bankIdFilter ??= (false, new List<int>());
            return await InternalGetInquiriesAsync(user, idFilter.Value, createDateFilter.Value, ammountFilter.Value, installmentFilter.Value, bankIdFilter.Value);
        }

        public async Task<IEnumerable<Inquiry>> InternalGetInquiriesAsync(
            User user,
            (bool IsRange, IList<int> arr) idFilter,
            (bool IsRange, IList<DateTime> arr) createDateFilter,
            (bool IsRange, IList<int> arr) ammountFilter,
            (bool IsRange, IList<int> arr) installmentFilter,
            (bool IsRange, IList<int> arr) bankIdFilter
            )
        {
            using LichvaContext db = new();
            var result = db.Inquiries
                .Include(x => x.Offer)
                .ThenInclude(x => x.History)
                .Include(x => x.Offer)
                .ThenInclude(x => x.OfferStatus)
                .Include(x => x.ForeignInquiry)
                .AsQueryable();

            if(user.RoleId != (await db.Roles.FirstAsync(x => x.Name == "employee")).Id)
                result = result.Where(x => x.UserId == user.Id);


            if (idFilter.IsRange)
                result = result.Where(inq =>
                    idFilter.arr.First() <= inq.Id && inq.Id <= idFilter.arr.Last()
                );
            else if (idFilter.arr.Count != 0)
                result = result.Where(inq =>
                    idFilter.arr.Contains(inq.Id)
                );

            if (createDateFilter.IsRange)
                result = result.Where(inq =>
                    createDateFilter.arr.First() <= inq.CreationDate &&
                        inq.CreationDate <= createDateFilter.arr.Last()
                );
            else if (createDateFilter.arr.Count != 0)
                result = result.Where(inq =>
                    inq.CreationDate.HasValue && 
                    createDateFilter.arr.Contains(inq.CreationDate.Value)
                );

            if (ammountFilter.IsRange)
                result = result.Where(inq =>
                    ammountFilter.arr.First() <= inq.Ammount && inq.Ammount <= ammountFilter.arr.Last()
                );
            else if (ammountFilter.arr.Count != 0)
                result = result.Where(inq =>
                    inq.Ammount.HasValue &&
                    ammountFilter.arr.Contains(inq.Ammount.Value)
                );

            if (installmentFilter.IsRange)
                result = result.Where(inq =>
                    installmentFilter.arr.First() <= inq.Installments && inq.Installments <= installmentFilter.arr.Last()
                );
            else if (installmentFilter.arr.Count != 0)
                result = result.Where(inq =>
                    inq.Installments.HasValue && 
                    installmentFilter.arr.Contains(inq.Installments.Value)
                );

            // TODO:
            //if (bankIdFilter.IsRange)
            //{
            //    var lichvaBank =  await db.Banks.FirstOrDefaultAsync(bank => bank.Name == "Lichva");
            //    int lichvaBankId = lichvaBank?.Id ?? 0;
            //    result.Include(x => x.ForeignInquiry);

            //    result = result.Where(inq =>
            //        (inq.ForeignInquiry != null && bankIdFilter.arr.First() <= inq.ForeignInquiry.BankId  && inq.ForeignInquiry.BankId <= bankIdFilter.arr.Last()) || 
            //        (inq.ForeignInquiry == null && bankIdFilter.arr.First() <= lichvaBankId  && lichvaBankId <= bankIdFilter.arr.Last())  
            //    );
            //}
            //else if (bankIdFilter.arr.Count != 0)
            //{
            //    var lichvaBank =  await db.Banks.FirstOrDefaultAsync(bank => bank.Name == "Lichva");
            //    int lichvaBankId = lichvaBank?.Id ?? 0;
            //    result.Include(x => x.ForeignInquiry);
            //    result = result.Where(inq =>
            //        (inq.ForeignInquiry == null && bankIdFilter.arr.Contains(lichvaBankId)) || 
            //        (inq.ForeignInquiry != null && bankIdFilter.arr.Contains(inq.ForeignInquiry.BankId))
            //    );
            //}

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

            ForeignInquiry fq = new ForeignInquiry
            {
                BankId = (await db.Banks.FirstAsync(x => x.Name == "Lichva")).Id,
                InquiryId = inquiry.Id,
                ForeignInquiryId = null
            };

            await db.ForeignInquiries.AddAsync(fq);
            await db.SaveChangesAsync();

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
            (bool IsRange, IList<int>)? idFilter = null,
            (bool IsRange, IList<int>)? inquiryIdFilter = null,
            (bool IsRange, IList<DateTime>)? createDateFilter = null,
            (bool IsRange, IList<decimal>)? percentageFilter = null,
            (bool IsRange, IList<decimal>)? monthlyInstallmentFilter = null,
            (bool IsRange, IList<int>)? statusFilter = null
            //(bool IsRange, IList<int>)? bankIdFiler = null,
            //(bool IsRange, IList<int>)? statusIdFilter = null,
            //(bool IsRange, IList<int>)? installmentsFilter = null,
            //(bool IsRange, IList<int>)? requestedValueFilter = null,
            //string? sortColumn = null,
            //bool? sortDesc = null
            )
        {
            idFilter??= (false, new List<int>());
            inquiryIdFilter ??= (false, new List<int>());
            createDateFilter??= (false, new List<DateTime>());
            percentageFilter ??= (false, new List<decimal>());
            monthlyInstallmentFilter ??= (false, new List<decimal>());
            statusFilter ??= (false, new List<int>());

            //return await InternalGetOffersAsync(user, idFilter.Value, inquiryIdFilter.Value, createDateFilter.Value, percentageFilter.Value, monthlyInstallmentFilter.Value, statusFilter.Value, bankIdFiler.Value, statusIdFilter.Value, installmentsFilter.Value, requestedValueFilter.Value, sortColumn, sortDesc);
            return await InternalGetOffersAsync(user, idFilter.Value, inquiryIdFilter.Value, createDateFilter.Value, percentageFilter.Value, monthlyInstallmentFilter.Value, statusFilter.Value);
        }

        public async Task<IEnumerable<Offer>> InternalGetOffersAsync(
            User user,
            (bool IsRange, IList<int> arr) idFilter,
            (bool IsRange, IList<int> arr) inquiryIdFilter,
            (bool IsRange, IList<DateTime> arr) createDateFilter,
            (bool IsRange, IList<decimal> arr) percentageFilter,
            (bool IsRange, IList<decimal> arr) monthlyInstallmentFilter,
            (bool IsRange, IList<int> arr) statusFilter
            //(bool IsRange, IList<int>) bankIdFiler,
            //(bool IsRange, IList<int>) statusIdFilter, 
            //(bool IsRange, IList<int>) installmentsFilter,
            //(bool IsRange, IList<int>) requestedValueFilter,
            //string? sortColumn = null,
            //bool? sortDesc = null
            )
        {
            using LichvaContext db = new();
            var result = db.Offers
                .Include(x => x.Inquiry)
                .Include(x => x.History)
                .Include(x => x.OfferStatus)
                .AsQueryable();

            if(user.RoleId != (await db.Roles.FirstAsync(x => x.Name == "employee")).Id)
                result = result.Where(x => x.Inquiry.UserId == user.Id);

            if (idFilter.IsRange)
                result = result.Where(x => idFilter.arr.First() <= x.Id && x.Id <= idFilter.arr.Last());
            else if(idFilter.arr.Count != 0)
                result = result.Where(x => idFilter.arr.Contains(x.Id));

            if (inquiryIdFilter.IsRange)
                result = result.Where(x => inquiryIdFilter.arr.First() <= x.InquiryId && x.InquiryId <= inquiryIdFilter.arr.Last());
            else if(inquiryIdFilter.arr.Count != 0)
                result = result.Where(x => x.InquiryId.HasValue && inquiryIdFilter.arr.Contains(x.InquiryId.Value));

            if (createDateFilter.IsRange)
                result = result.Where(x => createDateFilter.arr.First() <= x.CreationDate && x.CreationDate <= createDateFilter.arr.Last());
            else if(createDateFilter.arr.Count != 0)
                result = result.Where(x => x.CreationDate.HasValue && createDateFilter.arr.Contains(x.CreationDate.Value));

            if (percentageFilter.IsRange)
                result = result.Where(x => percentageFilter.arr.First() <= x.Percentage && x.Percentage <= percentageFilter.arr.Last());
            else if(percentageFilter.arr.Count != 0)
                result = result.Where(x => x.Percentage.HasValue && percentageFilter.arr.Contains(x.Percentage.Value));
            
            if (monthlyInstallmentFilter.IsRange)
                result = result.Where(x => monthlyInstallmentFilter.arr.First() <= x.MonthlyInstallment && x.MonthlyInstallment <= monthlyInstallmentFilter.arr.Last());
            else if(monthlyInstallmentFilter.arr.Count != 0)
                result = result.Where(x => x.MonthlyInstallment.HasValue && monthlyInstallmentFilter.arr.Contains(x.MonthlyInstallment.Value));

            if (statusFilter.IsRange)
                result = result.Where(x => statusFilter.arr.First() <= x.StatusId && x.StatusId <= statusFilter.arr.Last());
            else if(statusFilter.arr.Count != 0)
                result = result.Where(x => x.StatusId.HasValue && statusFilter.arr.Contains(x.StatusId.Value));



            //IQueryable<GetOfferDto> getOfferDtos = result.Select(x =>
            //new GetOfferDto
            //{
            //     Id = x.Id,
            //      Percentage = x.Percentage,
            //      MonthlyInstallment = x.MonthlyInstallment,
            //      Ammount = x.Inquiry.Ammount,
            //      Installments = 
            //}
            //);

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
                Internal = dto.Active,
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
            (bool IsRange, IList<int>)? idFilter = null, 
            (bool IsRange, IList<DateTime>)? createDateFilter = null, 
            (bool IsRange, IList<int>)? roleFilter = null, 
            (bool IsRange, IList<bool>)? internalFilter = null, 
            (bool IsRange, IList<bool>)? anonymousFilter = null, 
            (bool IsRange, IList<string>)? emailFilter = null,
            (bool IsRange, IList<string>)? hashFilter = null
            )
        {
            idFilter ??= (false, new List<int>());
            createDateFilter ??= (false, new List<DateTime>());
            roleFilter ??= (false, new List<int>());
            internalFilter ??= (false, new List<bool>());
            anonymousFilter ??= (false, new List<bool>());
            emailFilter ??= (false, new List<string>());
            hashFilter ??= (false, new List<string>());

            return await InternalGetUsersAsync(idFilter.Value, createDateFilter.Value, emailFilter.Value, roleFilter.Value, internalFilter.Value, anonymousFilter.Value, hashFilter.Value);
        }


        public async Task<IEnumerable<User>> InternalGetUsersAsync(
            (bool IsRange, IList<int> arr) idFilter,
            (bool IsRange, IList<DateTime> arr) createDateFilter,
            (bool IsRange, IList<string> arr) emailFilter,
            (bool IsRange, IList<int> arr) roleFilter,
            (bool IsRange, IList<bool> arr) internalFilter,
            (bool IsRange, IList<bool> arr) anonymousFilter,
            (bool IsRange, IList<string> arr) hashFilter
            )
        {
            using LichvaContext db = new();
            var result = db.Users.AsQueryable();

            if (idFilter.IsRange)
                result = result.Where(x => idFilter.arr.First() <= x.Id && x.Id <= idFilter.arr.Last());
            else if (idFilter.arr.Count != 0)
                result = result.Where(x => idFilter.arr.Contains(x.Id));
            
            if (createDateFilter.IsRange)
                result = result.Where(x => createDateFilter.arr.First() <= x.CreationDate && x.CreationDate <= createDateFilter.arr.Last());
            else if (createDateFilter.arr.Count != 0)
                result = result.Where(x =>
                    x.CreationDate.HasValue && createDateFilter.arr.Contains(x.CreationDate.Value)
                    );

            if (roleFilter.IsRange)
                result = result.Where(x => roleFilter.arr.First() <= x.RoleId && x.RoleId <= roleFilter.arr.Last());
            else if (roleFilter.arr.Count != 0)
                result = result.Where(x =>
                    x.RoleId.HasValue && roleFilter.arr.Contains(x.RoleId.Value)
                    );

            if (internalFilter.IsRange)
                result = result.Where(x => internalFilter.arr.First() == x.Internal && x.Internal == internalFilter.arr.Last());
            else if (internalFilter.arr.Count != 0)
                result = result.Where(x =>
                    x.Internal.HasValue && internalFilter.arr.Contains(x.Internal.Value)
                    );

            if (anonymousFilter.IsRange)
                result = result.Where(x => anonymousFilter.arr.First() == x.Anonymous && x.Anonymous == anonymousFilter.arr.Last());
            else if (anonymousFilter.arr.Count != 0)
                result = result.Where(x =>
                    x.Anonymous.HasValue && anonymousFilter.arr.Contains(x.Anonymous.Value)
                    );

            if (emailFilter.IsRange)
                result = result.Where(x => emailFilter.arr.First().CompareTo(x.Email) <= 0 && emailFilter.arr.Last().CompareTo(x.Email) >= 0);
            else if (emailFilter.arr.Count != 0)
                result = result.Where(x =>
                    x.Email != null && emailFilter.arr.Contains(x.Email)
                    );

            if (hashFilter.IsRange)
                result = result.Where(x => hashFilter.arr.First().CompareTo(x.Hash) <= 0 && hashFilter.arr.Last().CompareTo(x.Hash) >= 0);
            else if (hashFilter.arr.Count != 0)
                result = result.Where(x =>
                    x.Hash != null && hashFilter.arr.Contains(x.Hash)
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
            user.Internal = dto.Active;
            await db.SaveChangesAsync();
        }


        // //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public async Task<User?> AuthenticateUserAsync(string authToken)
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

        public async Task<Role?> GetRoleAsync(User user)
        {
            using LichvaContext db = new LichvaContext();
            return await db.Roles.FirstOrDefaultAsync(x => x.Id == user.RoleId);
        }

        public async Task<bool> AuthorizeUserAsync(User user, string roleName)
        {
            Role? role = await GetRoleAsync(user);
            return role != null && role.Name == roleName;
        }

        public async Task<IEnumerable<Offer>> GetUserOffersAsync(User user)
        {
            using LichvaContext db = new LichvaContext();

            var inquiryIds = db.Inquiries.Where(x => x.UserId == user.Id).Select(x => x.Id);

            IQueryable<int> foreignInqs = db.ForeignInquiries
                .Where(x => x.BankId == 1 && inquiryIds.Contains(x.InquiryId)).Select(x => x.InquiryId);

            var offers = await db.Offers.Where(x => foreignInqs.Contains(x.InquiryId.Value)).ToListAsync();

            foreignInqs = db.ForeignInquiries
                .Where(x => x.BankId != 1 && inquiryIds.Contains(x.InquiryId)).Select(x => x.ForeignInquiryId.Value);

            var CURRENTOTHERAPI = new List<Offer>();
            offers.AddRange(CURRENTOTHERAPI.Where(x => foreignInqs.Contains(x.InquiryId.Value)));

            return offers;
        }

        public async Task<IEnumerable<Inquiry>> GetUserInquiriesAsync(User user)
        {
            using LichvaContext db = new();
            return await db.Inquiries.Where(x => x.UserId == user.Id).ToListAsync();
        }

        public async Task<IEnumerable<Offer>> GetEmployeeOffersAsync(User user)
        {
            using LichvaContext db = new();
            var fq = db.ForeignInquiries.Where(x => x.BankId == 1).Select(x => x.InquiryId);
            return await db.Offers.Where(x => fq.Contains(x.InquiryId.Value)).ToListAsync();

            //var q = db.Offers
            //    .Include(x => x.Inquiry)
            //    .Include(x => x.OfferStatus)
            //    .Include(x => x.History)
            //    .Join(
            //        db.ForeignInquiries.Where(x => x.BankId == 1),
            //        l => l.InquiryId,
            //        r => r.InquiryId,
            //        (l, r) => new GetOfferDto
            //        {
            //            Id = l.Id,
            //            Percentage = l.Percentage,
            //            MonthlyInstallment = l.MonthlyInstallment,
            //            Ammount = l.Inquiry.Ammount,
            //            Installments = l.Inquiry.Installments,
            //            StatusId = l.StatusId,
            //            StatusDescription = l.OfferStatus.Name,
            //            InquiryId = l.InquiryId,
            //            CreateDate = l.CreationDate,
            //            UpdateDate = null,
            //            ApprovedBy = null,
            //            DocumentLink = l.DocumentLink,
            //            DocumentLinkValidDate = null,
            //            BankId = r.BankId
            //        });
        }

        public async Task<IEnumerable<Inquiry>> GetEmployeeInquiryAsync(User user)
        {
            using LichvaContext db = new();
            var usersId = db.Users.Where(x => x.Internal.Value).Select(x => x.Id);
            return await db.Inquiries.Where(x => usersId.Contains(x.UserId.Value)).ToListAsync();
        }

        //public async Task<OfferStatus?> CheckIdStatus(int stateId)
        //{
        //    using LichvaContext db = new LichvaContext();
        //    return await db.OfferStatuses.FirstOrDefaultAsync(x => x.Id == stateId);
        //}

        //public async Task UpdateOfferStatus(Offer offer, int newStatus)
        //{
        //    using LichvaContext db = new();
        //    var tmp = await db.Offers.FirstAsync(x => x.Id == offer.Id);

        //    tmp.StatusId = newStatus;
        //    await db.SaveChangesAsync();
        //}


        //public async Task<int> GetUsersCount()
        //{
        //    using LichvaContext db = new();
        //    return await db.Users.CountAsync();
        //}

    }
}

