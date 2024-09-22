using AutoMapper;
using LeaveManagement.Application.Exceptions;
using LeaveManagement.Application.Interfaces.Persistence;
using MediatR;

namespace LeaveManagement.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation;

public class DeleteLeaveAllocationCommandHandler : IRequestHandler<DeleteLeaveAllocationCommand, Unit>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IMapper _mapper;

    public DeleteLeaveAllocationCommandHandler(
        ILeaveAllocationRepository leaveAllocationRepository, 
        IMapper mapper)
    {
        _leaveAllocationRepository = leaveAllocationRepository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(
        DeleteLeaveAllocationCommand request, 
        CancellationToken cancellationToken)
    {
        var leaveAllocation = await _leaveAllocationRepository.GetLeaveAllocationWithDetails(request.Id);

        if (leaveAllocation == null)
            throw new NotFoundExceptions(nameof(LeaveAllocation), request.Id);
        
        await _leaveAllocationRepository.DeleteAsync(leaveAllocation);
        return Unit.Value;
    }
}