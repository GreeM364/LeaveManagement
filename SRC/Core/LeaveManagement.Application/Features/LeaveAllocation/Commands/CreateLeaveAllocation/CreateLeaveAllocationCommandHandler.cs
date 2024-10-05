using LeaveManagement.Application.Exceptions;
using LeaveManagement.Application.Interfaces.Identity;
using LeaveManagement.Application.Interfaces.Logging;
using LeaveManagement.Application.Interfaces.Persistence;
using MediatR;

namespace LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommandHandler : 
    IRequestHandler<CreateLeaveAllocationCommand, Unit>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IAppLogger<CreateLeaveAllocationCommandHandler> _logger;
    private readonly IUserService _userService;

    public CreateLeaveAllocationCommandHandler(
        ILeaveAllocationRepository leaveAllocationRepository, 
        ILeaveTypeRepository leaveTypeRepository, 
        IAppLogger<CreateLeaveAllocationCommandHandler> logger,
        IUserService userService)
    {
        _leaveAllocationRepository = leaveAllocationRepository;
        _leaveTypeRepository = leaveTypeRepository;
        _logger = logger;
        _userService = userService;
    }

    public async Task<Unit> Handle(
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
        var employees = await _userService.GetEmployeesAsync();
        var period = DateTime.Now.Year;

        var allocations = new List<Domain.LeaveAllocation>();
        foreach (var employee in employees)
        {
            var allocationExists =
                await _leaveAllocationRepository.AllocationExists(
                    userId: employee.Id,
                    leaveTypeId: leaveType.Id,
                    period: period);

            if (allocationExists == false)
            {
                var allocation = new Domain.LeaveAllocation
                {
                    EmployeeId = employee.Id,
                    LeaveTypeId = leaveType.Id,
                    NumberOfDays = leaveType.DefaultDays,
                    Period = period,
                };
                
                allocations.Add(allocation);
            }
        }

        if (allocations.Any())
        {
            await _leaveAllocationRepository.AddAllocations(allocations);   
        }
        
        return Unit.Value;
    }
}