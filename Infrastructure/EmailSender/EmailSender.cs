using Graft.Infrastructure.Watcher;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Graft.Infrastructure
{
    public class EmailSender : WatchableService, IEmailSender
    {
        readonly EmailSenderConfiguration _settings;

        public EmailSender(ILoggerFactory loggerFactory, IConfiguration configuration)
            : base(nameof(EmailSender), "Email Sender", loggerFactory, null, configuration)
        {
            _settings = configuration
                .GetSection("EmailSender")
                .Get<EmailSenderConfiguration>();
        }

        public override Task Ping()
        {
            return Task.CompletedTask;
        }

        public void SendEmail(string email, string subject, string htmlMessage)
        {
            var t = SendEmailAsync(email, subject, htmlMessage);
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var sw = new Stopwatch();
            sw.Start();

            try
            {
                await SendAsync(email, subject, htmlMessage);

                if (State != WatchableServiceState.OK)
                    SetState(WatchableServiceState.OK);
            }
            catch (Exception ex)
            {
                SetState(WatchableServiceState.Error, ex);
                throw;
            }
            finally
            {
                sw.Stop();
                LastOperationTime = DateTime.UtcNow;
                UpdateStopwatchMetrics(sw, State == WatchableServiceState.OK);
            }
        }

        async Task SendAsync(string email, string subject, string body)
        {
            using (var message = new MailMessage())
            {
                message.From = new MailAddress(_settings.Address, _settings.DisplayName);
                message.To.Add(new MailAddress(email));
                message.Body = body;
                message.IsBodyHtml = true;
                message.Subject = subject;
                message.SubjectEncoding = System.Text.Encoding.UTF8;

                using (var client = new SmtpClient(_settings.Server, _settings.Port))
                {
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(_settings.UserName, _settings.Password);

                    await client.SendMailAsync(message);
                }
            }
        }
    }
}
