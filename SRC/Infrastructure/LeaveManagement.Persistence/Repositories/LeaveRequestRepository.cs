using LeaveManagement.Application.Interfaces.Persistence;
using LeaveManagement.Domain;
using LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Persistence.Repositories;

public class LeaveRequestRepository 
    : Repository<LeaveRequest>, ILeaveRequestRepository
{
    public LeaveRequestRepository(
        LeaveManagementDatabaseContext context) : base(context)
    { }

    public async Task<LeaveRequest?> GetLeaveRequestWithDetails(Guid id)
    {
        var leaveRequest = await _context
            .LeaveRequests
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
        
        return leaveRequest;
    }

    public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails()
    {
        var leaveRequests = await _context
            .LeaveRequests
            .Include(x => x.LeaveType)
            .AsNoTracking()
            .ToListAsync();
        
        return leaveRequests;
    }

    public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails(Guid userId)
    {
        var leaveRequests = await _context
            .LeaveRequests
            .Where(q => q.RequestingEmployeeId == userId)
            .Include(q => q.LeaveType)
            .ToListAsync();
        
        return leaveRequests;
    }
}