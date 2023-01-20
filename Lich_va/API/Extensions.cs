using API.Dtos.Bank;
using API.Dtos.Dictionary;
using API.Dtos.Inquiry;
using API.Dtos.Offer;
using API.Dtos.User;
using API.Entities;
using API.Helpers;
using BankDataLibrary.Config;
using BankDataLibrary.Entities;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Remoting;
using System.Text;

namespace API
{
    public static class Extensions
    {

        // Inquiries
        public static GetInquiryDto AsGetDto(this Inquiry inquire)
        {
            using LichvaContext db = new LichvaContext();
            Offer? offer = db.Offers.Include(x => x.OfferStatus).FirstOrDefault(x => x.InquiryId == inquire.Id);

            GetInquiryDto result = new()
            {
                InquiryId = inquire.Id,
                CreationDate = inquire.CreationDate,
                Ammount = inquire.Ammount,
                Installments = inquire.Installments,
                StatusId = offer?.StatusId ?? null,
                StatusDescription = offer?.OfferStatus.Name ?? null,
                OfferId = offer?.Id ?? null,
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
            using LichvaContext db = new LichvaContext();
            OfferStatus status = db.OfferStatuses.First(x => x.Id == offer.StatusId);
            Inquiry inquiry = db.Inquiries.First(x => x.Id == offer.InquiryId);
            OfferHistory? history = db.OfferHistories.Where(x => x.OfferId == offer.Id).AsEnumerable().MaxBy(x => x.CreationDate);
            ForeignInquiry? fq = db.ForeignInquiries.FirstOrDefault(x => x.InquiryId == offer.InquiryId);
            GetOfferDto result = new()
            {
                Id = offer.Id,
                Percentage = offer.Percentage,
                MonthlyInstallment = offer.MonthlyInstallment,
                Ammount = inquiry.Ammount,
                Installments = inquiry.Installments,
                StatusId = offer.StatusId,
                StatusDescription = status.Name,
                InquiryId = offer.InquiryId,
                CreateDate = offer.CreationDate,
                UpdateDate = null,
                ApprovedBy = history?.EmployeeId ?? null,
                DocumentLink = offer.DocumentLink,
                DocumentLinkValidDate = null,
                BankId = fq?.BankId ?? null
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
                Anonymous = user.Anonymous,
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

        public static string GetContractName(this User user, Offer offer, string suffix)
        {
            return "umowa_" + offer.Id + suffix + ".";
        }

        public static (bool IsRange, IList<string> arr) Parse(this string? s)
        {
            if(s == null) return (false, new List<string>());

            var tmp = s.Trim().ToCharArray();
            StringBuilder builder = new StringBuilder();
            bool isRange = false;
            List<string> result = new();
            if(s.StartsWith("["))
            {
                isRange = true;
                builder.Append(tmp.Skip(1).TakeWhile(x => x != ',').ToArray());
                result.Add(builder.ToString());
                builder.Clear();
                builder.Append(tmp.SkipWhile(x => x != ',').Skip(1).SkipLast(1).ToArray());
                result.Add(builder.ToString());
            }
            else
            {
                var arr = s.Split(',');
                result = arr.ToList();
            }
            return (isRange, result);
        }
        public static (bool IsRange, IList<int> arr) ParseInt(this string? s)
        {
            if(s == null) return (false, new List<int>());

            var tmp = s.Trim().ToCharArray();
            StringBuilder builder = new StringBuilder();
            bool isRange = false;
            List<int> result = new();
            if(s.StartsWith("["))
            {
                isRange = true;
                builder.Append(tmp.Skip(1).TakeWhile(x => x != ',').ToArray());
                result.Add(int.Parse(builder.ToString()));
                builder.Clear();
                builder.Append(tmp.SkipWhile(x => x != ',').Skip(1).SkipLast(1).ToArray());
                result.Add(int.Parse(builder.ToString()));
            }
            else
            {
                var arr = s.Split(',');
                result = arr.Select(x => int.Parse(x)).ToList();
            }
            return (isRange, result);
        }

        public static (bool IsRange, IList<decimal> arr) ParseDecimal(this string? s)
        {
            if(s == null) return (false, new List<decimal>());

            var tmp = s.Trim().ToCharArray();
            StringBuilder builder = new StringBuilder();
            bool isRange = false;
            List<decimal> result = new();
            if(s.StartsWith("["))
            {
                isRange = true;
                builder.Append(tmp.Skip(1).TakeWhile(x => x != ',').ToArray());
                result.Add(decimal.Parse(builder.ToString()));
                builder.Clear();
                builder.Append(tmp.SkipWhile(x => x != ',').Skip(1).SkipLast(1).ToArray());
                result.Add(decimal.Parse(builder.ToString()));
            }
            else
            {
                var arr = s.Split(',');
                result = arr.Select(x => decimal.Parse(x)).ToList();
            }
            return (isRange, result);
        }

        public static (bool IsRange, IList<DateTime> arr) ParseDateTime(this string? s)
        {
            if(s == null) return (false, new List<DateTime>());

            var tmp = s.Trim().ToCharArray();
            StringBuilder builder = new StringBuilder();
            bool isRange = false;
            List<DateTime> result = new();
            if(s.StartsWith("["))
            {
                isRange = true;
                builder.Append(tmp.Skip(1).TakeWhile(x => x != ',').ToArray());
                result.Add(DateTime.Parse(builder.ToString()));
                builder.Clear();
                builder.Append(tmp.SkipWhile(x => x != ',').Skip(1).SkipLast(1).ToArray());
                result.Add(DateTime.Parse(builder.ToString()));
            }
            else
            {
                var arr = s.Split(',');
                result = arr.Select(x => DateTime.Parse(x)).ToList();
            }
            return (isRange, result);
        }
        public static (bool IsRange, IList<bool> arr) ParseBool(this string? s)
        {
            if(s == null) return (false, new List<bool>());

            var tmp = s.Trim().ToCharArray();
            StringBuilder builder = new StringBuilder();
            bool isRange = false;
            List<bool> result = new();
            if(s.StartsWith("["))
            {
                isRange = true;
                builder.Append(tmp.Skip(1).TakeWhile(x => x != ',').ToArray());
                result.Add(bool.Parse(builder.ToString()));
                builder.Clear();
                builder.Append(tmp.SkipWhile(x => x != ',').Skip(1).SkipLast(1).ToArray());
                result.Add(bool.Parse(builder.ToString()));
            }
            else
            {
                var arr = s.Split(',');
                result = arr.Select(x => bool.Parse(x)).ToList();
            }
            return (isRange, result);
        }

        public static IEnumerable<GetOfferDto> FilterOffers(
            this IEnumerable<GetOfferDto> query,
            string? creationDateFilter,
            string? requestedValueFilter,
            string? installmentsFilter,
            string? percentageFilter,
            string? monthlyInstallmentsFilter,
            string? bankIdFilter,
            string? statusIdFitler
            )
        {
            IEnumerable<GetOfferDto> result = query;

            if (creationDateFilter != null)
            {
                (var _, var range) = creationDateFilter.ParseDateTime();
                result = result.Where(x =>
                    range.First() <= x.CreateDate
                    && x.CreateDate <= range.Last());
            }

            if (requestedValueFilter != null)
            {
                (var _, var range) = requestedValueFilter.ParseInt();
                result = result.Where(x => range.First() <= x.Ammount && x.Ammount <= range.Last());
            }

            if (installmentsFilter != null)
            {
                (var _, var range) = installmentsFilter.ParseInt();
                result = result.Where(x => range.First() <= x.Installments && x.Installments <= range.Last());
            }

            if (percentageFilter != null)
            {
                (var _, var range) = percentageFilter.ParseDecimal();
                result = result.Where(x => range.First() <= x.Percentage && x.Percentage <= range.Last());
            }

            if (monthlyInstallmentsFilter != null)
            {
                (var _, var range) = percentageFilter.ParseDecimal();
                result = result.Where(x => range.First() <= x.MonthlyInstallment && x.MonthlyInstallment <= range.Last());
            }

            if (bankIdFilter != null)
            {
                (var _, var arr) = bankIdFilter.ParseInt();
                result = result.Where(x => arr.Contains(x.BankId.Value));
            }

            if (statusIdFitler != null)
            {
                (var _, var arr) = statusIdFitler.ParseInt();
                result = result.Where(x => arr.Contains(x.StatusId.Value));
            }

            return result;
        }

        public static IEnumerable<GetInquiryDto> FilterInquiries(
            this IEnumerable<GetInquiryDto> query,
            string? creationDateFilter,
            string? ammountFiler,
            string? installmentsFilter
            )
        {
            IEnumerable<GetInquiryDto> result = query;

            if (creationDateFilter != null)
            {
                (var _, var range) = creationDateFilter.ParseDateTime();
                result = result.Where(x =>
                    range.First() <= x.CreationDate
                    && x.CreationDate <= range.Last());
            }

            if (ammountFiler != null)
            {
                (var _, var range) = ammountFiler.ParseInt();
                result = result.Where(x => range.First() <= x.Ammount && x.Ammount <= range.Last());
            }

            if (installmentsFilter != null)
            {
                (var _, var range) = installmentsFilter.ParseInt();
                result = result.Where(x => range.First() <= x.Installments && x.Installments <= range.Last());
            }

            return result;
        }



    }
}
