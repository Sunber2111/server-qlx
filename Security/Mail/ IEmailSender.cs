using System.Threading.Tasks;
using API.Mail;

namespace API.Security.Mail
{
    public interface IEmailSender
    {
        Task SendEmailAsync(Message message);
    }
}