using FluentValidation;
using LeaveManagement.Application.Interfaces.Persistence;

namespace LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;

public class UpdateLeaveTypeCommandValidator : AbstractValidator<UpdateLeaveTypeCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public UpdateLeaveTypeCommandValidator(
        ILeaveTypeRepository leaveTypeRepository)
    {
        RuleFor(p => p.Id)
            .NotNull()
            .MustAsync(LeaveTypeMustExist);

        RuleFor(p => p.Name)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(70)
            .WithMessage("{PropertyName} must be fewer than 70 characters");

        RuleFor(p => p.DefaultDays)
            .LessThan(22)
            .WithMessage("{PropertyName} cannot exceed 22")
            .GreaterThan(1)
            .WithMessage("{PropertyName} cannot be less than 1");


        _leaveTypeRepository = leaveTypeRepository;
    }

    private async Task<bool> LeaveTypeMustExist(
        Guid id, 
        CancellationToken cancellationToken)
    {
        var leaveType = await _leaveTypeRepository.GetByIdAsync(id);
        return leaveType != null;
    }
}