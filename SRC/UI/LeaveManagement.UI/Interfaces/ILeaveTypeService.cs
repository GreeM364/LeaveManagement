using LeaveManagement.UI.Models.LeaveTypes;
using LeaveManagement.UI.Services.Base;

namespace LeaveManagement.UI.Interfaces;

public interface ILeaveTypeService
{
    Task<List<LeaveTypeViewModel>> GetLeaveTypesAsync();
    
    Task<LeaveTypeViewModel> GetLeaveTypeDetailsAsync(
        Guid id);
    
    Task<Response<Guid>> CreateLeaveTypeAsync(
        LeaveTypeViewModel leaveType);
    
    Task<Response<Guid>> UpdateLeaveTypeAsync(
        Guid id, 
        LeaveTypeViewModel leaveType);
    
    Task<Response<Guid>> DeleteLeaveTypeAsync(
        Guid id);
}