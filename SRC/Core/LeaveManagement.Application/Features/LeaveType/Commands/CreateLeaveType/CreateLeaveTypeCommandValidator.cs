using FluentValidation;
using LeaveManagement.Application.Interfaces.Persistence;

namespace LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;

public class CreateLeaveTypeCommandValidator 
    : AbstractValidator<CreateLeaveTypeCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public CreateLeaveTypeCommandValidator(
        ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;
        
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .MaximumLength(50)
            .WithMessage("{PropertyName} must not exceed 50 characters");
        
        RuleFor(x => x.DefaultDays)
            .GreaterThan(0)
            .WithMessage("{PropertyName} must be greater than 0")
            .LessThan(22)
            .WithMessage("{PropertyName} must be less than 22");
        
        RuleFor(x => x)
            .MustAsync(LeaveTypeNameUnique)
            .WithMessage("Leave type name already exists");
    }

    private async Task<bool> LeaveTypeNameUnique(
        CreateLeaveTypeCommand request,
        CancellationToken cancellationToken)
    {
        var isUnique = await _leaveTypeRepository
                .IsLeaveTypeUnique(request.Name);
        
        return isUnique;
    }
}