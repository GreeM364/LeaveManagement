using MediatR;

namespace LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;

public class GetLeaveAllocationListQuery : IRequest<List<LeaveAllocationDto>>
{ }