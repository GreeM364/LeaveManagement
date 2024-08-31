using LeaveManagement.Domain;

namespace LeaveManagement.Application.Interfaces.Persistence;

public interface ILeaveTypeRepository : IRepository<LeaveType>
{
    public Task<bool> IsLeaveTypeUnique(string name);
}