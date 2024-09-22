using MediatR;

namespace LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommand : IRequest<Guid>
{
    public Guid LeaveTypeId { get; set; }
}