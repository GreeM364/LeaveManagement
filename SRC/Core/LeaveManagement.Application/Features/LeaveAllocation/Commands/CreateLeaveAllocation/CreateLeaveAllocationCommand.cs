using MediatR;

namespace LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommand : IRequest<Unit>
{
    public Guid LeaveTypeId { get; set; }
}