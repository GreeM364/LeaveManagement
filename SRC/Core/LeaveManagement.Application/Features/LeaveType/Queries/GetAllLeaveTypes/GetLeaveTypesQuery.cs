using MediatR;

namespace LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

public class GetLeaveTypesQuery : IRequest<List<LeaveTypeDto>>
{ }