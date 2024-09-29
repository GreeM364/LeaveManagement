﻿using LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Identity.DatabaseContext;

public class LeaveManagementIdentityDatabaseContext : IdentityDbContext<ApplicationUser>
{
    public LeaveManagementIdentityDatabaseContext(
        DbContextOptions<LeaveManagementIdentityDatabaseContext> options)
            : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.ApplyConfigurationsFromAssembly(
            typeof(LeaveManagementIdentityDatabaseContext).Assembly);
    }
}