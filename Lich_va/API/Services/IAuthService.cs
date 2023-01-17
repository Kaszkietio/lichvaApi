using API;
using API.Entities;
using API.Repositories;
using BankDataLibrary.Entities;
using Google.Apis.Auth;
using System.Xml;

namespace GoogleAuth.Services
{
    public interface IAuthService
    {
        Task<User> Authenticate(Google.Apis.Auth.GoogleJsonWebSignature.Payload payload);
    }

    public class AuthService : IAuthService
    {
        public AuthService(IBankRepository repo)
        {
            Repository = repo;
            //Refresh();
        }

        //private void Refresh()
        //{
        //    if (_users.Count == 0)
        //    {
        //        _users.Add(new User() { id = Guid.NewGuid(), name = "Test Person1", email = "testperson1@gmail.com" });
        //        _users.Add(new User() { id = Guid.NewGuid(), name = "Test Person2", email = "testperson2@gmail.com" });
        //        _users.Add(new User() { id = Guid.NewGuid(), name = "Test Person3", email = "testperson3@gmail.com" });
        //        PrintUsers();
        //    }
        //}

        private static IList<User> _users = new List<User>();
        private IBankRepository Repository { get; set; }

        public async Task<User> Authenticate(GoogleJsonWebSignature.Payload payload)
        {
            return await FindUserOrAdd(payload);
        }

        private async Task<User> FindUserOrAdd(GoogleJsonWebSignature.Payload payload)
        {
            //var u = _users.Where(x => x.email == payload.Email).FirstOrDefault();
            //if (u == null)
            //{
            //    u = new User()
            //    {
            //        id = Guid.NewGuid(),
            //        name = payload.Name,
            //        email = payload.Email,
            //        oauthSubject = payload.Subject,
            //        oauthIssuer = payload.Issuer,
            //    };
            //    _users.Add(u);
            //}
            //PrintUsers();
            //return u;

            var user = await Repository.GetUserAsync(payload.Email);
            if(user == null)
            {
                user = new User()
                {
                    Email = payload.Email,
                    CreationDate = DateTime.Now,
                    FirstName = payload.GivenName,
                    LastName = payload.FamilyName,
                    Internal = false, 
                    Role = Category.UserRoleCategories.First().Name,
                    JobType = Category.UserJobCategories.First().Name,
                    IdType = Category.UserIdTypeCategories.First().Name,
                };
                Repository.CreateUserAsync(user).Wait();
            }

            Console.WriteLine($"Aud:[{payload.Audience}], Iss:[{payload.Issuer}]");

            return user;
        }

        //private static void PrintUsers()
        //{
        //    string s = string.Empty;
        //    //foreach (var u in _users) s += "\n[" + u.email + "]";
        //    //Console.WriteLine(s);
        //}
    }
}
