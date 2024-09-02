using LeaveManagement.Domain;

namespace LeaveManagement.Application.Interfaces.Persistence;

public interface ILeaveRequestRepository : IRepository<LeaveRequest>
{
    Task<LeaveRequest?> GetLeaveRequestWithDetails(Guid id);
    Task<List<LeaveRequest>> GetLeaveRequestsWithDetails();
    Task<List<LeaveRequest>> GetLeaveRequestsWithDetails(Guid userId);
}