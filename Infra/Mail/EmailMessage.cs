using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace Infra.Mail
{
    public class EmailMessage : IEmailMessage
    {
        public void SendEmail(string from, string to, string subject, string body, string smtpServer, int smtpPort)
        {
            SendEmail(from, to, subject, body, smtpServer, smtpPort, null);
        }

        public void SendEmail(string from, string to, string subject, string body, string smtpServer, int smtopPort, string[] attachments)
        {
            #region
            if (string.IsNullOrWhiteSpace(smtpServer))
                throw new ArgumentException("O servidor SMTP não pode ser nulo");

            MimeMessage email = new();
            email.From.Add(new MailboxAddress("email", from));
            email.To.Add(new MailboxAddress("email", to));
            email.Subject = subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = body };

            var bodyBuilder = new BodyBuilder
            {
                TextBody = body
            };

            if (attachments != null)
            {
                foreach (string filePath in attachments)
                {
                    var attachment = new MimeKit.MimePart()
                    {
                        Content = new MimeKit.MimeContent(File.OpenRead(filePath)),
                        ContentDisposition = new MimeKit.ContentDisposition(MimeKit.ContentDisposition.Attachment),
                        ContentTransferEncoding = MimeKit.ContentEncoding.Base64,
                        FileName = Path.GetFileName(filePath)
                    };

                    bodyBuilder.Attachments.Add(attachment);
                }
            }


            using var smtpClient = new SmtpClient();
            try
            {
                email.Body = bodyBuilder.ToMessageBody();

                smtpClient.Connect(smtpServer, smtopPort, SecureSocketOptions.StartTls);
                smtpClient.Authenticate("tech.challenge23@gmail.com", "qgflnkoghhhbjydg");
                smtpClient.Send(email);
            }
            catch (SmtpCommandException ex)
            {
                var statusCode = ex.StatusCode;
                if (statusCode == SmtpStatusCode.MailboxBusy ||
                    statusCode == SmtpStatusCode.MailboxUnavailable ||
                    statusCode == SmtpStatusCode.TransactionFailed)
                {
                    Thread.Sleep(5000);
                    smtpClient.Send(email);
                }
                throw;
            }
            #endregion

        }

    }
}
