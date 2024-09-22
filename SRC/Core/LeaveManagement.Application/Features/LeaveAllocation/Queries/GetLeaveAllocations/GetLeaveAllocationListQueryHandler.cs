using AutoMapper;
using LeaveManagement.Application.Interfaces.Persistence;
using MediatR;

namespace LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;

public class GetLeaveAllocationListQueryHandler : 
    IRequestHandler<GetLeaveAllocationListQuery, List<LeaveAllocationDto>>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IMapper _mapper;

    public GetLeaveAllocationListQueryHandler(
        ILeaveAllocationRepository leaveAllocationRepository, 
        IMapper mapper)
    {
        _leaveAllocationRepository = leaveAllocationRepository;
        _mapper = mapper;
    }

    public async Task<List<LeaveAllocationDto>> Handle(
        GetLeaveAllocationListQuery request, 
        CancellationToken cancellationToken)
    {
        var leaveAllocations = 
                await _leaveAllocationRepository.GetLeaveAllocationsWithDetails();
        
        var result = _mapper.Map<List<LeaveAllocationDto>>(leaveAllocations);
        
        return result;
    }
}