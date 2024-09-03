using LeaveManagement.Application.Interfaces.Email;
using LeaveManagement.Application.Interfaces.Logging;
using LeaveManagement.Application.Models.Email;
using LeaveManagement.Infrastructure.EmailService;
using LeaveManagement.Infrastructure.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace LeaveManagement.Infrastructure;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection ConfigureInfrastructureServices(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        services.AddTransient<IEmailSender, EmailSender>();
        services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
        
        return services;
    }
}