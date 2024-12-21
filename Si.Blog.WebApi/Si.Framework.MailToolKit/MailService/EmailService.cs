using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Si.Framework.MailToolKit;

namespace Si.Framework.MailToolKit.MailService
{
    public class EmailService : IEmailService
    {
        private SmtpSettings smtpSettings;
        private int retryCount;
        private int MaxRelayMillSeconds;

        public EmailService(IConfiguration configuration, int retryCount = 3, int MaxRelayMillSeconds = 1000)
        {
            var section = configuration.GetSection("mailConfig");
            smtpSettings = new SmtpSettings()
            {
                SmtpServer = section.GetValue<string>("SmtpServer"),
                SmtpPort = section.GetValue<int>("SmtpPort"),
                UseSsl = section.GetValue<bool>("UseSsl"),
                SenderEmail = section.GetValue<string>("SenderEmail"),
                SenderName = section.GetValue<string>("SenderName"),
                Password = section.GetValue<string>("Password")
            };
            this.retryCount = retryCount;
            this.MaxRelayMillSeconds = MaxRelayMillSeconds;
        }

        public void SetSmtpSetting(SmtpSettings smtpSettings)
        {
            this.smtpSettings = smtpSettings;
        }
        public async Task SendEmailAsync(IEnumerable<Recipient> toEmails,
        string subject,
        string body,
        bool isHtml = true,
        IEnumerable<Recipient> ccEmails = null,
        IEnumerable<Recipient> bccEmails = null,
        IEnumerable<string> attachments = null)
        {
            var message = CreateEmailMessage(toEmails, subject, body, isHtml, ccEmails, bccEmails, attachments);
            await SendWithRetryAsync(message);
        }
        public async Task SendSimpleEmailAsync(Recipient toEmail, string subject, string body)
        {
            await SendEmailAsync(new List<Recipient> { toEmail }, subject, body, isHtml: false);
        }

        public async Task SendHtmlEmailAsync(Recipient toEmail, string subject, string htmlBody)
        {
            await SendEmailAsync(new List<Recipient> { toEmail }, subject, htmlBody, isHtml: true);
        }
        private MimeMessage CreateEmailMessage(
             IEnumerable<Recipient> toEmails,
             string subject,
             string body,
             bool isHtml,
             IEnumerable<Recipient> ccEmails = null,
             IEnumerable<Recipient> bccEmails = null,
             IEnumerable<string> attachmentsAddress = null)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(smtpSettings.SenderName, smtpSettings.SenderName));
            var recipients = toEmails ?? null;
            if (recipients != null)
            {
                message.To.AddRange(recipients.Select(email => new MailboxAddress(email.ToName, email.MailAddress)));

            }
            var ccList = ccEmails ?? null;
            if (ccList != null)
            {
                message.Cc.AddRange(ccList.Select(email => new MailboxAddress(email.ToName, email.MailAddress)));
            }

            if (bccEmails != null)
            {
                message.Bcc.AddRange(bccEmails.Select(email => new MailboxAddress(email.ToName, email.MailAddress)));
            }
            message.Subject = subject;
            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = isHtml ? body : null,
                TextBody = !isHtml ? body : null
            };
            if (attachmentsAddress != null)
            {
                foreach (var filePath in attachmentsAddress)
                {
                    if (File.Exists(filePath))
                    {
                        bodyBuilder.Attachments.Add(filePath);
                    }
                }
            }
            message.Body = bodyBuilder.ToMessageBody();
            return message;
        }
        private async Task SendWithRetryAsync(MimeMessage message)
        {
            int attempt = 0;
            bool success = false;

            while (attempt < retryCount && !success)
            {
                using var client = new MailKit.Net.Smtp.SmtpClient();
                try
                {
                    await client.ConnectAsync(smtpSettings.SmtpServer, smtpSettings.SmtpPort, smtpSettings.UseSsl ? SecureSocketOptions.SslOnConnect : SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(smtpSettings.SenderEmail, smtpSettings.Password);
                    await client.SendAsync(message);
                    success = true;  // 邮件发送成功

                }
                catch (Exception ex)
                {
                    attempt++;
                    if (attempt >= retryCount)
                    {
                        throw new Exception("Failed to send email after multiple attempts.", ex);
                    }
                    // 等待指定时间后重试
                    await Task.Delay(MaxRelayMillSeconds);
                }
                finally
                {
                    await client.DisconnectAsync(true);
                }
            }
        }
    }

}
