using API;
using API.Dtos.User;
using API.Entities;
using API.Repositories;
using BankDataLibrary.Entities;
using Google.Apis.Auth;
using System.Xml;

namespace GoogleAuth.Services
{
    public interface IAuthService
    {
        Task<OnUserCreationDto> Authenticate(Google.Apis.Auth.GoogleJsonWebSignature.Payload payload);
    }

    public class AuthService : IAuthService
    {
        public AuthService(IBankRepository repo)
        {
            Repository = repo;
        }

        private static IList<User> _users = new List<User>();
        private IBankRepository Repository { get; set; }

        public async Task<OnUserCreationDto> Authenticate(GoogleJsonWebSignature.Payload payload)
        {
            return await FindUserOrAdd(payload);
        }

        private async Task<OnUserCreationDto> FindUserOrAdd(GoogleJsonWebSignature.Payload payload)
        {
            var user = (await Repository.GetUsersAsync(emailFilter: (false, new List<string> { payload.Email }))).FirstOrDefault();
            OnUserCreationDto result;
            if (user == null || (!user.Internal.Value && !user.Anonymous.Value))
            {
                var newUser = new CreateUserDto()
                {
                    RoleId = Repository.GetRolesAsync().Result.First().Id,
                    JobTypeId = (await Repository.GetJobTypesAsync()).First(x => x.Name == "none").Id,
                    IdTypeId = (await Repository.GetJobTypesAsync()).First(x => x.Name == "none").Id,    
                    Active = false,
                    Anonymous = true,
                    Email = payload.Email,
                    FirstName = payload.GivenName ?? string.Empty,
                    LastName = payload.FamilyName ?? string.Empty,
                };
                result = await Repository.CreateUserAsync(newUser);
            }
            else
                result = user.AsOnCreationDto();

            //Console.WriteLine($"Aud:[{payload.Audience}], Iss:[{payload.Issuer}]");

            return result;
        }
    }
}
