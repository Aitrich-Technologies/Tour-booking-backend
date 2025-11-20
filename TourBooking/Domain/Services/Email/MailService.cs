using Domain.Helper;
using Domain.Services.Email.Helper;
using Domain.Services.Email.Interface;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System.Threading.Tasks;

namespace Domain.Services.Email
{
    public class MailService : IMailService
    {
        private readonly MailSettings _settings;

        public MailService(MailSettings settings)
        {
            _settings = settings;
        }

        //public async Task SendEmailAsync(MailRequest request)
        //{
        //    var email = new MimeMessage();
        //    email.From.Add(new MailboxAddress(_settings.DisplayName, _settings.UserMail));
        //    email.To.Add(MailboxAddress.Parse(request.ToEmail));
        //    email.Subject = request.Subject;
        //    email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

        //    // ✅ Use MailKit's SmtpClient
        //    using var smtp = new MailKit.Net.Smtp.SmtpClient();
        //    await smtp.ConnectAsync(_settings.Host, _settings.Port,
        //        _settings.UseSSL ? SecureSocketOptions.SslOnConnect : SecureSocketOptions.StartTls);
        //    await smtp.AuthenticateAsync(_settings.UserMail, _settings.Password);
        //    await smtp.SendAsync(email);
        //    await smtp.DisconnectAsync(true);
        //}
        public async Task SendEmailAsync(MailRequest request)
        {
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress(_settings.DisplayName, _settings.UserMail));
            email.To.Add(MailboxAddress.Parse(request.ToEmail));
            email.Subject = request.Subject;

            var builder = new BodyBuilder
            {
                HtmlBody = request.Body
            };

            // ➤ Add attachments if present
            if (request.Attachments != null)
            {
                foreach (var attachment in request.Attachments)
                {
                    builder.Attachments.Add(
                        attachment.FileName,
                        attachment.FileBytes,
                        ContentType.Parse(attachment.ContentType)
                    );
                }
            }

            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();

            await smtp.ConnectAsync(
                _settings.Host,
                _settings.Port,
                _settings.UseSSL ? SecureSocketOptions.SslOnConnect : SecureSocketOptions.StartTls
            );

            await smtp.AuthenticateAsync(_settings.UserMail, _settings.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }

    }
}
