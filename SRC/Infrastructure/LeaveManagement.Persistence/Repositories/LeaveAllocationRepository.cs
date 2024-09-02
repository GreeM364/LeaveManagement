using LeaveManagement.Application.Interfaces.Persistence;
using LeaveManagement.Domain;
using LeaveManagement.Persistence.DatabaseContext;

namespace LeaveManagement.Persistence.Repositories;

public class LeaveAllocationRepository 
    : Repository<LeaveAllocation>, ILeaveAllocationRepository
{
    public LeaveAllocationRepository(LeaveManagementDatabaseContext context) 
        : base(context)
    { }
}