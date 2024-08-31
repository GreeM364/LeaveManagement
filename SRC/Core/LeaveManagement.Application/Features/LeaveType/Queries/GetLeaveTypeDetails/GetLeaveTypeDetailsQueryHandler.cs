using AutoMapper;
using LeaveManagement.Application.Exceptions;
using LeaveManagement.Application.Interfaces.Persistence;
using MediatR;

namespace LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;

public class GetLeaveTypeDetailsQueryHandler 
    : IRequestHandler<GetLeaveTypeDetailsQuery, LeaveTypeDetailsDto>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    
    public GetLeaveTypeDetailsQueryHandler(
        IMapper mapper, 
        ILeaveTypeRepository leaveTypeRepository)
    {
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
    }
    
    public async Task<LeaveTypeDetailsDto> Handle(
        GetLeaveTypeDetailsQuery request, 
        CancellationToken cancellationToken)
    {
        var leaveTypeDetails = await _leaveTypeRepository
                .GetByIdAsync(request.LeaveTypeId);
        
        if (leaveTypeDetails == null)
        {
            throw new NotFoundExceptions(nameof(LeaveType), request.LeaveTypeId);
        }
        
        var result = _mapper.Map<LeaveTypeDetailsDto>(leaveTypeDetails);

        return result;
    }
}