using API.Dtos.Bank;
using API.Dtos.Dictionary;
using API.Dtos.Inquiry;
using API.Dtos.Offer;
using API.Dtos.User;
using API.Entities;
using API.Helpers;
using BankDataLibrary.Entities;
using System.Runtime.Remoting;

namespace API
{
    public static class Extensions
    {

        // Inquiries
        public static GetInquiryDto AsGetDto(this Inquiry inquire)
        {
            Offer? offer = inquire.Offer;

            GetInquiryDto result = new()
            {
                Ammount = inquire.Ammount,
                Installments = inquire.Installments,
                StatusId = offer?.StatusId ?? null,
                StatusDescription = offer.OfferStatus?.Name ?? null,
                Offer = offer?.AsGetDto() ?? null,
            };
            return result;
        }
        public static OnInquiryCreationDto AsOnCreationDto(this Inquiry inquire)
        {
            OnInquiryCreationDto result = new()
            {
                InquireId = inquire.Id,
                CreatedDate = inquire.CreationDate,
                Data = inquire.AsGetDto(),
            };
            return result;
        }
         
        // Offers
        public static GetOfferDto AsGetDto(this Offer offer)
        {
            GetOfferDto result = new()
            {
                Id = offer.Id,
                Percentage = offer.Percentage,
                MonthlyInstallment = offer.MonthlyInstallment,
                RequestedValue = offer.Inquiry.Ammount,
                RequestedPeriodInMonth = offer.Inquiry.Installments,
                StatusId = offer.StatusId,
                StatusDescription = offer.OfferStatus.Name,
                InquiryId = offer.InquiryId,
                CreateDate = offer.CreationDate,
                UpdateDate = null,
                ApprovedBy = offer.History.MaxBy(x => x.CreationDate)?.EmployeeId,
                DocumentLink = offer.DocumentLink,
                DocumentLinkValidDate = null,
            };
            return result;
        }

        public static GetBankDto AsGetDto(this Bank bank)
        {
            GetBankDto result = new()
            {
                Id = bank.Id,
                Name = bank.Name,
            };
            return result;
        }

        public static GetUserDto AsGetDto(this User user)
        {
            GetUserDto result = new()
            {
                CreationDate = user.CreationDate,
                Email = user.Email,
                FirstName = user.FirstName,
                IdNumber = user.IdNumber,
                IdTypeId = user.IdTypeId,
                IncomeLevel = user.IncomeLevel,
                JobTypeId = user.JobTypeId,
                LastName = user.LastName,
                RoleId = user.RoleId,
                Active = user.Internal,
            };
            return result;
        }
        public static OnUserCreationDto AsOnCreationDto(this User user)
        {
            OnUserCreationDto result = new()
            {
                Id = user.Id,
                CreationDate = user.CreationDate,
                Data = user.AsGetDto(),
            };
            return result;
        }

        public static DictionaryDto AsDto(this Role obj)
        {
            DictionaryDto result = new()
            {
                Id = obj.Id,
                Name = obj.Name,
                Description = null,
            };
            return result;
        }

        public static DictionaryDto AsDto(this JobType obj)
        {
            DictionaryDto result = new()
            {
                Id = obj.Id,
                Name = obj.Name,
                Description = obj.Description,
            };
            return result;
        }

        public static DictionaryDto AsDto(this IdType obj)
        {
            DictionaryDto result = new()
            {
                Id = obj.Id,
                Name = obj.Name,
                Description = obj.Description,
            };
            return result;
        }

        public static DictionaryDto AsDto(this OfferStatus obj)
        {
            DictionaryDto result = new()
            {
                Id = obj.Id,
                Name = obj.Name,
                Description = null,
            };
            return result;
        }

        public static string GetContractName(this User user, Offer offer)
        {
            return Security.Encrypt(AppSettings.Instance.ContractKey, "umowa_"
                    + user.Email
                    + "_"
                    + offer.Id.ToString());
        }

    }
}
