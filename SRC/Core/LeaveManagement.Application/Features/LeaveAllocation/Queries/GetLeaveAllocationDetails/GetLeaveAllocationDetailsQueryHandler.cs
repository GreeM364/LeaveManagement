using AutoMapper;
using LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;
using LeaveManagement.Application.Interfaces.Persistence;
using MediatR;

namespace LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;

public class GetLeaveAllocationDetailsQueryHandler : 
    IRequestHandler<GetLeaveAllocationDetailsQuery, LeaveAllocationDetailsDto>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IMapper _mapper;

    public GetLeaveAllocationDetailsQueryHandler(
        ILeaveAllocationRepository leaveAllocationRepository, 
        IMapper mapper)
    {
        _leaveAllocationRepository = leaveAllocationRepository;
        _mapper = mapper;
    }
    
    public async Task<LeaveAllocationDetailsDto> Handle(
        GetLeaveAllocationDetailsQuery request, 
        CancellationToken cancellationToken)
    {
        var leaveAllocation = await _leaveAllocationRepository
                .GetLeaveAllocationWithDetails(request.Id);
        
        var allocation = _mapper.Map<LeaveAllocationDetailsDto>(leaveAllocation);
        
        return allocation;
    }
}