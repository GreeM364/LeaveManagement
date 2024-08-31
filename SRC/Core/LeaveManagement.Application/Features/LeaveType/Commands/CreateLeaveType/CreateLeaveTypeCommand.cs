using MediatR;

namespace LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;

public class CreateLeaveTypeCommand : IRequest<Guid>
{
    public string Name { get; set; } = string.Empty;
    public int DefaultDays { get; set; }
}