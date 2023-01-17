﻿using API.Dtos;
using BankDataLibrary.Entities;

namespace API
{
    public static class Extensions
    {
        public static InquiryDto AsDto(this Inquiry inquire)
        {
            InquiryDto result = new()
            {
                Id = inquire.Id,
                CreationDate = inquire.CreationDate,
                UserId = inquire.UserId,
                Ammount = inquire.Ammount,
                Installments = inquire.Installments,
            };
            return result;
        }
        public static OfferDto AsDto(this Offer offer)
        {
            OfferDto result = new()
            {
                Id = offer.Id,
                CreationDate = offer.CreationDate,
                UserId = offer.UserId,
                Ammount = offer.Ammount,
                Installments = offer.Installments,
                BankId = offer.BankId,
                PlatformId = offer.PlatformId,
                Status = offer.Status,
            };
            return result;
        }

        public static BankDto AsDto(this Bank bank)
        {
            BankDto result = new()
            {
                Id = bank.Id,
                CreationDate = bank.CreationDate,
                Name = bank.Name,
            };
            return result;
        }

        public static UserDto AsDto(this User user)
        {
            UserDto result = new()
            {
                CreationDate = user.CreationDate,
                Email = user.Email,
                FirstName = user.FirstName,
                Hash = user.Hash,
                IdNumber = user.IdNumber,
                IdType = user.IdType,
                IncomeLevel = user.IncomeLevel,
                JobType = user.JobType,
                LastName = user.LastName,
                Role = user.Role,
            };
            return result;
        }
    }
}
