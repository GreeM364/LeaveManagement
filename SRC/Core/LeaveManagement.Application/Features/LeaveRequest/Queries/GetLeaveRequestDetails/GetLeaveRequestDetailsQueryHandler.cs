using AutoMapper;
using LeaveManagement.Application.Exceptions;
using LeaveManagement.Application.Interfaces.Persistence;
using MediatR;

namespace LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails;

public class GetLeaveRequestDetailsQueryHandler : 
    IRequestHandler<GetLeaveRequestDetailsQuery, LeaveRequestDetailsDto>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IMapper _mapper;

    public GetLeaveRequestDetailsQueryHandler(
        ILeaveRequestRepository leaveRequestRepository,
        IMapper mapper)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _mapper = mapper;
    }
    
    public async Task<LeaveRequestDetailsDto> Handle(
        GetLeaveRequestDetailsQuery request, 
        CancellationToken cancellationToken)
    {
        var leaveRequest = 
                await _leaveRequestRepository.GetLeaveRequestWithDetails(request.Id);
        
        var result = _mapper.Map<LeaveRequestDetailsDto>(leaveRequest);

        if (result == null)
            throw new NotFoundException(nameof(LeaveRequest), request.Id);
        
        return result;
    }
}