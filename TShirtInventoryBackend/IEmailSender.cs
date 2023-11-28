using TshirtInventoryBackend.Models;

namespace TshirtInventoryBackend
{
    public interface IEmailSender
    {
        void SendEmail(IEnumerable<User> users, string productName, int quantity);
    }
}
