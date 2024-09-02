using LeaveManagement.Domain;

namespace LeaveManagement.Application.Interfaces.Persistence;

public interface ILeaveAllocationRepository : IRepository<LeaveAllocation>
{
    Task<LeaveAllocation?> GetLeaveAllocationWithDetails(Guid id);
    Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails();
    Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails(Guid userId);
    Task<bool> AllocationExists(
        Guid userId, 
        Guid leaveTypeId, 
        int period);
    Task AddAllocations(List<LeaveAllocation> allocations);
    Task<LeaveAllocation?> GetUserAllocations(
        Guid userId, 
        Guid leaveTypeId);
}