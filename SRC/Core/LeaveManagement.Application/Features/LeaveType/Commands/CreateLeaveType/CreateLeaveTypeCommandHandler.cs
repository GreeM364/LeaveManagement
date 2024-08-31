using AutoMapper;
using LeaveManagement.Application.Interfaces.Persistence;
using MediatR;

namespace LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;

public class CreateLeaveTypeCommandHandler 
    : IRequestHandler<CreateLeaveTypeCommand, Guid>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public CreateLeaveTypeCommandHandler(
        IMapper mapper, 
        ILeaveTypeRepository leaveTypeRepository)
    {
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
    }

    public async Task<Guid> Handle(
        CreateLeaveTypeCommand request, 
        CancellationToken cancellationToken)
    {
        var leaveType = _mapper.Map<Domain.LeaveType>(request);
        
        await _leaveTypeRepository.CreateAsync(leaveType);
        
        return leaveType.Id;
    }
}