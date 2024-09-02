using LeaveManagement.Application.Interfaces.Persistence;
using LeaveManagement.Domain;
using LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Persistence.Repositories;

public class LeaveAllocationRepository 
    : Repository<LeaveAllocation>, ILeaveAllocationRepository
{
    public LeaveAllocationRepository(
        LeaveManagementDatabaseContext context) : base(context)
    { }

    public async Task<LeaveAllocation?> GetLeaveAllocationWithDetails(Guid id)
    {
        var leaveAllocation = await _context
            .LeaveAllocations
            .Include(q => q.LeaveType)
            .FirstOrDefaultAsync(q => q.Id == id);

        return leaveAllocation;
    }

    public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails()
    {
        var leaveAllocations = await _context
            .LeaveAllocations
            .Include(q => q.LeaveType)
            .ToListAsync();
        
        return leaveAllocations;
    }

    public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails(Guid userId)
    {
        var leaveAllocations = await _context
            .LeaveAllocations
            .Where(q => q.EmployeeId == userId)
            .Include(q => q.LeaveType)
            .ToListAsync();
        return leaveAllocations;
    }

    public async Task<bool> AllocationExists(
        Guid userId, 
        Guid leaveTypeId, 
        int period)
    {
        var isExist = await _context
            .LeaveAllocations
            .AnyAsync(q => 
                    q.EmployeeId == userId && 
                    q.LeaveTypeId == leaveTypeId && 
                    q.Period == period);

        return isExist;
    }

    public async Task AddAllocations(List<LeaveAllocation> allocations)
    {
        await _context.AddRangeAsync(allocations);
        await _context.SaveChangesAsync();
    }

    public async Task<LeaveAllocation?> GetUserAllocations(
        Guid userId, 
        Guid leaveTypeId)
    {
        var leaveAllocation = await _context
            .LeaveAllocations
            .FirstOrDefaultAsync(q => 
                q.EmployeeId == userId && 
                q.LeaveTypeId == leaveTypeId);

        return leaveAllocation;
    }
}