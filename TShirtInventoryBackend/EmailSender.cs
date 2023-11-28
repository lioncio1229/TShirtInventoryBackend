using MailKit.Net.Smtp;
using MimeKit;
using TshirtInventoryBackend.Models;

namespace TshirtInventoryBackend
{
    public class EmailSender : IEmailSender
    {
        private readonly SmtpClient _client;
        public EmailSender() 
        {
            _client = new SmtpClient();
            _client.Connect("smtp.gmail.com", 465, true);
            _client.Authenticate("lionciomorcilla.aws@gmail.com", "vmxz ygyu vlrr baza");
        }

        public void SendEmail(IEnumerable<User> users, string productName, int quantity)
        {
            foreach (var user in users) {
                using (var message = new MimeMessage())
                {
                    message.From.Add(new MailboxAddress("TshirtInventory", "lionciomorcilla.aws@gmail.com"));
                    message.To.Add(new MailboxAddress(user.FullName, user.Email));
                    message.Subject = "Product Low Stock";

                    message.Body = new TextPart("plain")
                    {
                        Text = $@"Hi {user.FullName},

                        Exciting news – our {productName} is flying off the shelves, and there are only {quantity} left! 
                        Don't miss out – grab yours before it's gone. 🚀

                        Cheers,
                        Tshirt Inventory
                        "
                    };

                    _client.Send(message);
                }
            }
        }

        public void Dispose()
        {
            _client.Disconnect(true);
            _client.Dispose();
        }
    }
}
