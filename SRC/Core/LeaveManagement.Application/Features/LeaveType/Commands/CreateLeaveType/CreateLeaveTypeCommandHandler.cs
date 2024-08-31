using AutoMapper;
using LeaveManagement.Application.Exeptions;
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
        var validator = new CreateLeaveTypeCommandValidator(_leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(
                request, 
                cancellationToken);

        if (validationResult.Errors.Any())
        {
            throw new BadRequestExceptions("Invalid LeaveType", validationResult);
        }
        
        var leaveType = _mapper.Map<Domain.LeaveType>(request);
        
        await _leaveTypeRepository.CreateAsync(leaveType);
        
        return leaveType.Id;
    }
}