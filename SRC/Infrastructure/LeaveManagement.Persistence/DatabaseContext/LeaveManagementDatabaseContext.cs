using LeaveManagement.Domain;
using LeaveManagement.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Persistence.DatabaseContext;

public class LeaveManagementDatabaseContext : DbContext
{
    public DbSet<LeaveType> LeaveTypes { get; set; }
    public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
    public DbSet<LeaveRequest> LeaveRequests { get; set; }
    
    public LeaveManagementDatabaseContext(
        DbContextOptions<LeaveManagementDatabaseContext> options) : base(options)
    { }

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(LeaveManagementDatabaseContext).Assembly);
        
        base.OnModelCreating(modelBuilder);    
    }
    
    public override async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        var entries= base.ChangeTracker
            .Entries<BaseEntity>()
            .Where(entry => 
                entry.State == EntityState.Added || 
                entry.State == EntityState.Modified);
        
        foreach (var entry in entries)
        {
            entry.Entity.DateModified = DateTime.Now;

            if (entry.State == EntityState.Added)
            {
                entry.Entity.DateCreated = DateTime.Now;
            }
        }
        
        return await base.SaveChangesAsync(cancellationToken);
    }
}