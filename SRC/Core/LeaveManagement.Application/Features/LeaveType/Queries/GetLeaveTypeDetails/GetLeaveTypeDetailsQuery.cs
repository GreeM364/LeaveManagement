using MediatR;

namespace LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;

public class GetLeaveTypeDetailsQuery : IRequest<LeaveTypeDetailsDto>
{
    public Guid LeaveTypeId { get; set; }
}