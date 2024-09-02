using LeaveManagement.Application.Interfaces.Persistence;
using LeaveManagement.Domain;
using LeaveManagement.Persistence.DatabaseContext;

namespace LeaveManagement.Persistence.Repositories;

public class LeaveRequestRepository 
    : Repository<LeaveRequest>, ILeaveRequestRepository
{
    public LeaveRequestRepository(LeaveManagementDatabaseContext context) 
        : base(context)
    { }
}