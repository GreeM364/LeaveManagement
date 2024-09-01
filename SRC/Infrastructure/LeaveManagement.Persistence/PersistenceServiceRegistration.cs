using LeaveManagement.Persistence.Configuration;
using LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LeaveManagement.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<LeaveManagementDatabaseContext>(options =>
        {
            options.UseSqlServer(
                configuration.GetConnectionString("LeaveManagementConnectionString"));
        });
        
        return services;
    }
}