using LeaveManagement.Application.Interfaces.Persistence;
using LeaveManagement.Domain;
using LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Persistence.Repositories;

public class LeaveTypeRepository : Repository<LeaveType>, ILeaveTypeRepository
{
    public LeaveTypeRepository(
        LeaveManagementDatabaseContext context) : base(context)
    { }

    public async Task<bool> IsLeaveTypeUnique(string name)
    {
        var isUnique = await _context
            .LeaveTypes
            .AnyAsync(q => q.Name == name) == false;
        
        return isUnique;
    }
}