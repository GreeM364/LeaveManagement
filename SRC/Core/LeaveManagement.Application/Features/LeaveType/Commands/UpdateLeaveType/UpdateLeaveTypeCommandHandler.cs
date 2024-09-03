using AutoMapper;
using LeaveManagement.Application.Exeptions;
using LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using LeaveManagement.Application.Interfaces.Logging;
using LeaveManagement.Application.Interfaces.Persistence;
using MediatR;

namespace LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;

public class UpdateLeaveTypeCommandHandler 
    : IRequestHandler<UpdateLeaveTypeCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IAppLogger<CreateLeaveTypeCommandHandler> _logger;

    public UpdateLeaveTypeCommandHandler(
        IMapper mapper, 
        ILeaveTypeRepository leaveTypeRepository, 
        IAppLogger<CreateLeaveTypeCommandHandler> logger)
    {
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(
        UpdateLeaveTypeCommand request, 
        CancellationToken cancellationToken)
    {
        var validator = new UpdateLeaveTypeCommandValidator(_leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(
            request, 
            cancellationToken);

        if (validationResult.Errors.Any())
        {
            _logger.LogWarning(
                "Validation errors in update request fpr {0} - 1", 
                nameof(Domain.LeaveType), 
                request.Id);
            
            throw new BadRequestExceptions(
                "Invalid Leave type", 
                validationResult);
        }
        
        var leaveTypeToUpdate = _mapper.Map<Domain.LeaveType>(request);
        
        await _leaveTypeRepository.UpdateAsync(leaveTypeToUpdate);
        
        return Unit.Value;
    }
}