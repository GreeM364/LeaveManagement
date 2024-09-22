using MediatR;

namespace LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval;

public class ChangeLeaveRequestApprovalCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public bool Approved { get; set; }
}