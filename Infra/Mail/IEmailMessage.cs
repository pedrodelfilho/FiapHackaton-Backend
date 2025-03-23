namespace Infra.Mail
{
    public interface IEmailMessage
    {
        void SendEmail(string from, string to, string subject, string body, string smtpServer, int smtpPort);
        void SendEmail(string from, string to, string subject, string body, string smtpServer, int smtopPort, string[] attachments);
    }
}
