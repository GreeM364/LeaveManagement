using LeaveManagement.Application.Features.LeaveRequest.Shared;
using MediatR;

namespace LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;

public class CreateLeaveRequestCommand : BaseLeaveRequest, IRequest<Guid>
{
    public string RequestComments { get; set; } = string.Empty;
}