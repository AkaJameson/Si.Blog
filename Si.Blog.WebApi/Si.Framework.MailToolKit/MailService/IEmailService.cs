using Si.Framework.MailToolKit;

namespace Si.Framework.MailToolKit.MailService
{
    public interface IEmailService
    {
        Task SendEmailAsync(IEnumerable<Recipient> toEmails, string subject, string body, bool isHtml = true, IEnumerable<Recipient> ccEmails = null, IEnumerable<Recipient> bccEmails = null, IEnumerable<string> attachments = null);
        Task SendHtmlEmailAsync(Recipient toEmail, string subject, string htmlBody);
        Task SendSimpleEmailAsync(Recipient toEmail, string subject, string body);
        void SetSmtpSetting(SmtpSettings smtpSettings);
    }
}