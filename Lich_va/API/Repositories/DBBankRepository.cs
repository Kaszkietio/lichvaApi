﻿using API.Dtos;
using BankDataLibrary.Config;
using BankDataLibrary.Entities;
using Microsoft.EntityFrameworkCore;
using static API.Repositories.IBankRepository;

namespace API.Repositories
{
    public class DBBankRepository : IBankRepository
    {
        public async Task CreateInquiryAsync(Inquiry inquiry)
        {
            using LichvaContext db = new LichvaContext();
            await db.AddAsync(inquiry);
            await db.SaveChangesAsync();
        }

        public async Task CreateLoginHistoryAsync(LoginHistory login)
        {
            using LichvaContext db = new LichvaContext();
            await db.AddAsync(login);
            await db.SaveChangesAsync();
        }

        public async Task CreateOfferAsync(Offer offer)
        {
            using LichvaContext db = new LichvaContext();
            await db.AddAsync(offer);
            await db.SaveChangesAsync();
        }

        public async Task CreateOfferHistoryAsync(OfferHistory offer)
        {
            using LichvaContext db = new LichvaContext();
            await db.AddAsync(offer);
            await db.SaveChangesAsync();
        }

        public async Task CreateUserAsync(User user)
        {
            using LichvaContext db = new LichvaContext();
            await db.AddAsync(user);
            await db.SaveChangesAsync();
        }

        public async Task<Bank?> GetBankAsync(int bankId)
        {
            using LichvaContext db = new LichvaContext();
            return await db.Banks
                .FirstOrDefaultAsync(bank => bank.Id == bankId);
        }

        public async Task<IEnumerable<Bank>> GetBanksAsync()
        {
            using LichvaContext db = new LichvaContext();
            return await db.Banks
                .ToListAsync();
        }

        public async Task<IEnumerable<Inquiry>> GetInquiriesAsync(int? inqId = null, int? userId = null)
        {
            using LichvaContext db = new LichvaContext();
            return await db.Inquiries
                .Where(inq => inqId == null || inqId == inq.Id)
                .Where(inq => userId == null || userId == inq.UserId)
                .ToListAsync();
        }


    public async Task<IEnumerable<LoginHistory>> GetLoginHistoriesAsync()
        {
            using LichvaContext db = new LichvaContext();
            return await db.LoginHistories
                .ToListAsync();
        }

        public async Task<LoginHistory?> GetLoginHistoryAsync(int userId)
        {
            using LichvaContext db = new LichvaContext();
            return await db.LoginHistories
                .FirstOrDefaultAsync(log => log.UserId== userId);
        }

        public async Task<Offer?> GetOfferAsync(int userId)
        {
            using LichvaContext db = new LichvaContext();
            return await db.Offers.FirstOrDefaultAsync(offer => offer.UserId == userId);
        }

        public async Task<IEnumerable<OfferHistory>> GetOfferHistoryAsync(int? userId = null)
        {
            using LichvaContext db = new LichvaContext();
            return await db.OfferHistories.ToListAsync();
        }

        public async Task<IEnumerable<Offer>> GetOffersAsync(int? userId = null, int? inquiryId = null, int? bankId = null)
        {
            using LichvaContext db = new LichvaContext();
            IQueryable<Offer> filter = db.Offers;
            if(userId != null)
                filter = filter.Where(offer => offer.UserId == userId);
            if (inquiryId != null)
                filter = filter.Where(offer => offer.Id == inquiryId);
            if(bankId != null)
                filter = filter.Where(offer => offer.BankId == bankId);

            return await filter.ToListAsync();
        }

        public async Task<User?> GetUserAsync(int userId)
        {
            using LichvaContext db = new LichvaContext();
            return await db.Users.FirstOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<User?> GetUserAsync(string email)
        {
            using LichvaContext db = new LichvaContext();
            return await db.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            using LichvaContext db = new LichvaContext();
            return await db.Users.ToListAsync();
        }

        public async Task UpdateOfferAsync(Offer offer)
        {
            using LichvaContext db = new LichvaContext();
            Offer? current = await db.Offers.FirstOrDefaultAsync(x => x.Id == offer.Id);
            if (current == null) return;

            db.Entry(current).CurrentValues.SetValues(offer);

            //current.UserId = offer.Id;
            //current.BankId = offer.BankId;
            //current.PlatformId = offer.PlatformId;
            //current.Ammount = offer.Ammount;
            //current.Installments = offer.Installments;

            await db.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(UpdateUserDto user)
        {
            using LichvaContext db = new LichvaContext();
            User? current = await db.Users.FirstOrDefaultAsync(x => x.Email == user.Email);
            if (current == null) return;

            db.Entry(current).CurrentValues.SetValues(user);
            current.Internal = user.Active;

            await db.SaveChangesAsync();
        }
    }
}

