using LeaveManagement.UI.Services.Base;

namespace LeaveManagement.UI.Interfaces;

public interface ILeaveAllocationService
{
    Task<Response<Guid>> CreateLeaveAllocations(Guid leaveTypeId);
}