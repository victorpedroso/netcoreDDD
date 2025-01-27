using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using TechSolutions.Application.Interfaces;
using TechSolutions.Application.Models;

namespace TechSolutions.Application.Services;

public class EmailService(IConfiguration config) : IEmailService
{
    public bool Send(SendEmailModel sendEmail)
    {
        try
        {
            string host = config["SMTP:Host"];
            string display = config["SMTP:Display"];
            string userName = config["SMTP:UserName"];
            string password = config["SMTP:Password"];
            int port = Convert.ToInt32(config["SMTP:Port"]);

            MailMessage mail = new MailMessage
            {
                From = new MailAddress(userName, display),
            };

            mail.To.Add(sendEmail.Email);
            mail.Subject = sendEmail.Subject;
            mail.Body = sendEmail.Body;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;

            using SmtpClient smtp = new SmtpClient(host, port);
            smtp.Credentials = new NetworkCredential(userName, password);
            smtp.EnableSsl = true;

            smtp.Send(mail);

            return true;
        }
        catch
        {
            return false;
        }
    }
}
