using MediatR;

namespace LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;

public class DeleteLeaveTypeCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}