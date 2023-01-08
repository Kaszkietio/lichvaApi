﻿using BankDataLibrary.Entities;

namespace API.Repositories
{
    public interface IBankRepository
    {
        public void CreateInquire(Inquire inquire);

        public Inquire? GetInquire(int id);

        public IEnumerable<Inquire> GetInquires();

        public Offer? GetOffer(int offerId);
        public IEnumerable<Offer> GetOffers();
        public void CreateOffer(Offer offer);

        /// <summary>
        /// Checks whether provided API token is valid (is generated by our platform)
        /// </summary>
        /// <param name="APIToken">Token to be examined</param>
        /// <returns><c>True</c> if token is valid, <c>False</c> on the contrary.</returns>
        public void CheckForPlatformPermission(string APIToken);

        /// <summary>
        /// Checks whether user with provided email exists.
        /// </summary>
        /// <param name="email">Potentially ones users email</param>
        /// <returns><c>True</c> if user is valid, <c>False</c> on the contrary.</returns>
        public void CheckIfUserExists(string email);

        /// <summary>
        /// Method, which returns valid API token, of user with provided email.
        /// </summary>
        /// <param name="email">Ones users email.</param>
        /// <returns>Valid APIToken if user exists, otherwise throws.</returns>
        /// <exception cref="InvalidCredentialException"/>
        public string GetUserAPIToken(string email);

        /// <summary>
        /// Creates a new user with provided parameters and returns created entity on success.<br/>
        /// TODO: nie wiem jak jeszcze z tym GID jaki typ etc, dodatkowo nwm czy rzucamy bledem czy zwracamy User?,
        /// w dotnet 6 z tego co widze do klas rowniez powinno sie dawac ? w przypadku nullable obiektow
        /// </summary>
        /// <param name="email">New users email</param>
        /// <param name="GID">New users group ID</param>
        /// <returns>Created <c>User</c> entity on success.</returns>
        public User CreateUser(string email, string GID);

        /// <summary>
        /// Logs in logs about user login :))
        /// </summary>
        /// <param name="APIToken">Ones users login</param>
        /// <returns>Result of the opertion - state is in Result</returns>
        public void LoginUser(string APIToken);

        /// <summary>
        /// Checks whether user with specified API token already exists and has been registered.
        /// </summary>
        /// <param name="APIToken">Ones users token</param>
        /// <returns><c>True</c> if user registerable, <c>False</c> otherwise.</returns>
        public bool CheckIfUserRegisterable(string APIToken);

        /// <summary>
        /// Registers user with specified APIToken.
        /// </summary>
        /// <param name="APIToken">Ones users API token</param>
        /// <param name="firstName">First name of the user.</param>
        /// <param name="lastName">Last name of the user.</param>
        /// <param name="jobType">Job type listed in JobCategories enum.</param>
        /// <param name="incomeLevel"></param>
        /// <param name="idType"></param>
        /// <param name="idNumber"></param>
        /// <returns></returns>
        public User RegisterUser(string APIToken, 
                                         string firstName, 
                                         string lastName, 
                                         User.JobCategories jobType,
                                         double incomeLevel,
                                         User.IdTypes idType,
                                         string idNumber);

        /// <summary>
        /// Checks whether user with provided API token exists.
        /// </summary>
        /// <param name="APIToken">Ones users API token</param>
        /// <returns><c>True</c> if user exists, <c>False</c> otherwise.</returns>
        public bool CheckForUser(string APIToken);

        /// <summary>
        /// Returns users data, which is going to be displayed.
        /// </summary>
        /// <param name="APIToken">Ones users API token.</param>
        /// <returns>If succeeded - valid object UserPanelData, otherwise exception in void object.</returns>
        public UserPanelData GetUserPanelData(string APIToken);

        ///// <summary>
        ///// Currently suspended :((
        ///// </summary>
        ///// <param name="callAPIToken"></param>
        ///// <param name="offset"></param>
        ///// <param name="sorts"></param>
        ///// <param name="filter"></param>
        ///// <returns></returns>
        //public List<Inquiry> GetInquires(string callAPIToken, int offset, string[] sorts, string filter);

        /// <summary>
        /// Get list of offers of user with provided APIToken. There are possible filtering options.
        /// </summary>
        /// <param name="callAPIToken">Ones users API token</param>
        /// <param name="offset">Offset for void query</param>
        /// <param name="sorts">We will see</param>
        /// <param name="filter">We will see</param>
        /// <returns></returns>
        public List<Offer> GetOffers(string callAPIToken, int offset, string[] sorts, string filter);

        /// <summary>
        /// Checks whether provided API Token belongs to bank
        /// </summary>
        /// <param name="APIToken">Ones banks API token</param>
        /// <returns><c>True</c> if good old jewish bank, <c>False</c> otherwise.</returns>
        public bool CheckForBank(string APIToken);

        /// <summary>
        /// Returns status of the offer corresponding to the provided offerID.
        /// </summary>
        /// <param name="offerID">Id of the offer</param>
        /// <returns></returns>
        public Offer.Status GetOfferStatus(int offerID);

        /// <summary>
        /// Returns data of the user corresponding to the provided API Token.
        /// </summary>
        /// <param name="APIToken">Ones users API Token</param>
        /// <returns></returns>
        public User GetUserData(string APIToken);

        /// <summary>
        /// Creates external user in repository
        /// </summary>
        /// <param name="userData"></param>
        /// <returns><c>OK</c> if success, <c>Error</c> if sth went wrong.</returns>
        public User CreateExternalUser(UserData userData);

        /// <summary>
        /// Returns users id corresponding to provided email.
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        public int GetUserID(string mail);

        /// <summary>
        /// Returns whether user with provided email exists.
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        public bool SearchForUser(string mail);

        /// <summary>
        /// Returns all initial offers from all banks.
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="installments"></param>
        /// <param name="ammount"></param>
        /// <returns></returns>
        public List<Offer> GetAllInitialOffers(int userID, int installments, int ammount);

        /// <summary>
        /// Returns new calculated offer for provided requirements
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="installments"></param>
        /// <param name="ammount"></param>
        /// <returns></returns>
        public Offer CreateInitialOffer(int userID, int installments, int ammount);

        /// <summary>
        /// Creates document for client and returns the link
        /// </summary>
        /// <param name="offerID"></param>
        /// <returns></returns>
        public string CreateDoc(int offerID);

        /// <summary>
        /// Uploads new signed document to offer corresponding to offerID
        /// </summary>
        /// <param name="offerID"></param>
        /// <param name="signedDoc"></param>
        /// <returns></returns>
        public void UploadDoc(int offerID, byte[] signedDoc);

        /// <summary>
        /// Sends email with info about updated offer status to user
        /// </summary>
        /// <param name="offerID"></param>
        /// <returns></returns>
        public void SendUpdateEmail(int offerID);

        /// <summary>
        /// Checks whether current user is bank employee.
        /// </summary>
        /// <param name="APIToken"></param>
        /// <returns></returns>
        public bool CheckForEmployee(string APIToken);

        /// <summary>
        /// Returns list of extended offers with applied filters
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="sorts"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<Offer> GetExtendedOffers(int offset, string[] sorts, string filter);

        /// <summary>
        /// Changes status of the offer corresponding to offerID
        /// </summary>
        /// <param name="offerID"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public void ChangeOfferStatus(int offerID, Offer.Status status);

        /// <summary>
        /// Checks whether current user is admin user
        /// </summary>
        /// <param name="APIToken"></param>
        /// <returns></returns>
        public bool CheckForAdmin(string APIToken);

        /// <summary>
        /// Returns list of extended users with applied filters
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="sorts"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<User> GetExtendedUsers(int offset, string[] sorts, string filter);

        /// <summary>
        /// Changes role of the user corresponding to the userID
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public void ChangleRole(int userID, User.Role role);
    }
}
