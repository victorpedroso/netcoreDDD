using TechSolutions.Application.Models;

namespace TechSolutions.Application.Interfaces;

public interface IEmailService
{
    bool Send(SendEmailModel sendEmail);
}
