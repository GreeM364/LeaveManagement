using FluentValidation;
using LeaveManagement.Application.Interfaces.Persistence;

namespace LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;

public class UpdateLeaveAllocationCommandValidator : AbstractValidator<UpdateLeaveAllocationCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;

    public UpdateLeaveAllocationCommandValidator(
        ILeaveTypeRepository leaveTypeRepository, 
        ILeaveAllocationRepository leaveAllocationRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;
        _leaveAllocationRepository = leaveAllocationRepository;
        
        RuleFor(p => p.NumberOfDays)
            .GreaterThan(0)
            .WithMessage("{PropertyName} must greater than {ComparisonValue}");

        RuleFor(p => p.Period)
            .GreaterThanOrEqualTo(DateTime.Now.Year)
            .WithMessage("{PropertyName} must be after {ComparisonValue}");

        RuleFor(p => p.LeaveTypeId)
            .NotNull()
            .MustAsync(LeaveTypeMustExist)
            .WithMessage("{PropertyName} does not exist.");

        RuleFor(p => p.Id)
            .NotNull()
            .MustAsync(LeaveAllocationMustExist)
            .WithMessage("{PropertyName} must be present");
    }

    private async Task<bool> LeaveAllocationMustExist(
        Guid id, 
        CancellationToken cancellationToken)
    {
        var leaveAllocation = await _leaveAllocationRepository.GetByIdAsync(id);
        return leaveAllocation != null;
    }

    private async Task<bool> LeaveTypeMustExist(
        Guid id, 
        CancellationToken cancellationToken)
    {
        var leaveType = await _leaveTypeRepository.GetByIdAsync(id);
        return leaveType != null;
    }
}