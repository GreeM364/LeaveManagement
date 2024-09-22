using AutoMapper;
using LeaveManagement.Application.Exceptions;
using LeaveManagement.Application.Interfaces.Logging;
using LeaveManagement.Application.Interfaces.Persistence;
using MediatR;

namespace LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommandHandler : 
    IRequestHandler<CreateLeaveAllocationCommand, Guid>
{
    private readonly IMapper _mapper;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IAppLogger<CreateLeaveAllocationCommandHandler> _logger;

    public CreateLeaveAllocationCommandHandler(
        IMapper mapper, 
        ILeaveAllocationRepository leaveAllocationRepository, 
        ILeaveTypeRepository leaveTypeRepository, 
        IAppLogger<CreateLeaveAllocationCommandHandler> logger)
    {
        _mapper = mapper;
        _leaveAllocationRepository = leaveAllocationRepository;
        _leaveTypeRepository = leaveTypeRepository;
        _logger = logger;
    }

    public async Task<Guid> Handle(
        CreateLeaveAllocationCommand request, 
        CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveAllocationCommandValidator(_leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(
                request, 
                cancellationToken);
        
        if (validationResult.Errors.Any())
        {
            var allErrors = string
                .Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));

            _logger.LogError(
                "Validation errors in create request for {0} - {1}. Errors: {2}", 
                nameof(Domain.LeaveAllocation), 
                request.LeaveTypeId, 
                allErrors);
            
            throw new BadRequestException(
                "Invalid Leave Allocation Request", 
                validationResult);
        }

        var leaveType = await _leaveTypeRepository.GetByIdAsync(request.LeaveTypeId);

        var leaveAllocation = _mapper.Map<Domain.LeaveAllocation>(request);
        await _leaveAllocationRepository.CreateAsync(leaveAllocation);
        
        return leaveAllocation.Id;
    }
}