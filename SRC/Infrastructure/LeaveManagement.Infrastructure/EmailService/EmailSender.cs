using System.Net;
using LeaveManagement.Application.Interfaces.Email;
using LeaveManagement.Application.Models.Email;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace LeaveManagement.Infrastructure.EmailService;

public class EmailSender : IEmailSender
{
    private readonly EmailSettings _emailSettings;
    
    public EmailSender(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }
    
    public async Task<bool> SendEmailAsync(EmailMessage email)
    {
        var client = new SendGridClient(_emailSettings.ApiKey);
        var to = new EmailAddress(email.To);
        var from = new EmailAddress
        {
            Email = _emailSettings.FromAddress,
            Name = _emailSettings.FromName
        };
        
        var message = MailHelper.CreateSingleEmail(
            from: from, 
            to: to, 
            subject: email.Subject, 
            plainTextContent: email.Body, 
            htmlContent: email.Body);
        
        var response = await client.SendEmailAsync(message);

        var statusCode = response.IsSuccessStatusCode;
        
        return statusCode;
    }
}