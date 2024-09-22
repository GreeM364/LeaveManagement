using MediatR;

namespace LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;

public class UpdateLeaveAllocationCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public int NumberOfDays { get; set; }
    public Guid LeaveTypeId { get; set; }
    public int Period { get; set; }
}