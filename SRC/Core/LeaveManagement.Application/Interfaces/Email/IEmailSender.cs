using LeaveManagement.Application.Models.Email;

namespace LeaveManagement.Application.Interfaces.Email;

public interface IEmailSender
{
    Task<bool> SendEmailAsync(EmailMessage email);
}