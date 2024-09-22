using AutoMapper;
using LeaveManagement.Application.Exeptions;
using LeaveManagement.Application.Interfaces.Logging;
using LeaveManagement.Application.Interfaces.Persistence;
using MediatR;

namespace LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;

public class CreateLeaveTypeCommandHandler 
    : IRequestHandler<CreateLeaveTypeCommand, Guid>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IAppLogger<CreateLeaveTypeCommandHandler> _logger;

    public CreateLeaveTypeCommandHandler(
        IMapper mapper, 
        ILeaveTypeRepository leaveTypeRepository, 
        IAppLogger<CreateLeaveTypeCommandHandler> logger)
    {
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
        _logger = logger;
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
            var allErrors = string
                .Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));

            _logger.LogError(
                "Validation errors in create request for {0} - {1}. Errors: {2}", 
                nameof(Domain.LeaveType), 
                request.Name, 
                allErrors);
            
            throw new BadRequestExceptions(
                "Invalid LeaveType", 
                validationResult);
        }
        
        var leaveType = _mapper.Map<Domain.LeaveType>(request);
        
        await _leaveTypeRepository.CreateAsync(leaveType);
        
        return leaveType.Id;
    }
}