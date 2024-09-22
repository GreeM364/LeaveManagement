using AutoMapper;
using LeaveManagement.Application.Interfaces.Persistence;
using MediatR;

namespace LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequests;

public class GetLeaveRequestsQueryHandler : IRequestHandler<GetLeaveRequestsQuery, List<LeaveRequestsDto>>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IMapper _mapper;

    public GetLeaveRequestsQueryHandler(
        ILeaveRequestRepository leaveRequestRepository, 
        IMapper mapper)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _mapper = mapper;
    }

    public async Task<List<LeaveRequestsDto>> Handle(
        GetLeaveRequestsQuery request, 
        CancellationToken cancellationToken)
    {
        var leaveRequests = 
                await _leaveRequestRepository.GetLeaveRequestsWithDetails();
        
        var result = _mapper.Map<List<LeaveRequestsDto>>(leaveRequests);
        
        return result;
    }
}