using AutoMapper;
using LeaveManagement.Application.Exceptions;
using LeaveManagement.Application.Interfaces.Logging;
using LeaveManagement.Application.Interfaces.Persistence;
using MediatR;

namespace LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;

public class UpdateLeaveAllocationCommandHandler : IRequestHandler<UpdateLeaveAllocationCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IAppLogger<UpdateLeaveAllocationCommandHandler> _logger;

    public UpdateLeaveAllocationCommandHandler(
        IMapper mapper, 
        ILeaveAllocationRepository leaveAllocationRepository, 
        ILeaveTypeRepository leaveTypeRepository, 
        IAppLogger<UpdateLeaveAllocationCommandHandler> logger)
    {
        _mapper = mapper;
        _leaveAllocationRepository = leaveAllocationRepository;
        _leaveTypeRepository = leaveTypeRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(
        UpdateLeaveAllocationCommand request, 
        CancellationToken cancellationToken)
    {
        var validator = new UpdateLeaveAllocationCommandValidator(
                _leaveTypeRepository,
                _leaveAllocationRepository);
        var validationResult = await validator.ValidateAsync(
                request, 
                cancellationToken);
        
        if (validationResult.Errors.Any())
        {
            var allErrors = string
                .Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));

            _logger.LogError(
                "Validation errors in update request for {0} - {1}. Errors: {2}", 
                nameof(Domain.LeaveAllocation), 
                request.LeaveTypeId, 
                allErrors);
            
            throw new BadRequestException(
                "Invalid Leave Allocation Request", 
                validationResult);
        }

        var leaveAllocation = await _leaveAllocationRepository.GetByIdAsync(request.Id);

        if (leaveAllocation is null)
            throw new NotFoundException(nameof(Domain.LeaveAllocation), request.Id);

        _mapper.Map(request, leaveAllocation);
        
        await _leaveAllocationRepository.UpdateAsync(leaveAllocation);
        return Unit.Value;
    }
}