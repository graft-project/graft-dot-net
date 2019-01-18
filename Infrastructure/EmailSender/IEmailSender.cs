using Graft.Infrastructure.Watcher;
using System.Threading.Tasks;

namespace Graft.Infrastructure
{
    public interface IEmailSender : IWatchableService
    {
        void SendEmail(string email, string subject, string htmlMessage);
        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}