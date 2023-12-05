using MailKit.Net.Smtp;
using MimeKit;
using SchoolProject.Data.Helpers;
using SchoolProject.Service.Abstractions;

namespace SchoolProject.Service.Implemintations
{
    public class EmailService : IEmailService
    {
        private readonly EmailSetting _emailSetting;
        public EmailService(EmailSetting emailSetting)
        {
            _emailSetting = emailSetting;
        }
        public async Task<string> SendEmail(string email, string Message, string? reason)
        {
            try
            {
                //sending the Message of passwordResetLink
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_emailSetting.Host, _emailSetting.Port, true);
                    client.Authenticate(_emailSetting.FromEmail, _emailSetting.Password);
                    var bodybuilder = new BodyBuilder
                    {
                        HtmlBody = $"{Message}",
                        TextBody = "wellcome",
                    };
                    var message = new MimeMessage
                    {
                        Body = bodybuilder.ToMessageBody(),
                    };
                    message.From.Add(new MailboxAddress("school Team", _emailSetting.FromEmail));
                    message.To.Add(new MailboxAddress("testing", email));
                    message.Subject = reason == null ? "No Submitted" : reason;
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);

                    //end of sending email
                    return "Success";
                }

            }
            catch (Exception ex)
            {
                return "Failed";
            }
        }
    }
}

