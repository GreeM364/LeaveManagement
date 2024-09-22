using MediatR;

namespace LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequests;

public class GetLeaveRequestsQuery : IRequest<List<LeaveRequestsDto>>
{ }