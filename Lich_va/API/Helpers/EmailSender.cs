using API.Entities;
using BankDataLibrary.Entities;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace API.Helpers
{
    public class EmailSender
    {
        public static async Task SendEmail(User user)
        {
            var apiKey = AppSettings.Instance.FunnyLittleString;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("lichva.loancomparer@gmail.com", "Lich.va");
            var subject = "Status twojego zapytania się zmienił!";
            var to = new EmailAddress(user.Email, user.FirstName + " " + user.LastName);
            var plainTextContent = 
                $"Witaj {user.FirstName}!\n" +
                $"Status jednego z Twoich zapytań uległ właśnie zmianie. " +
                $"Koniecznie odwiedź swój profil i sprawdź czy to nie oferta dla Ciebie!\n" +
                $"Pozdrawiamy\n" +
                $"Zespół Lich.va";
            var htmlContent = "";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
