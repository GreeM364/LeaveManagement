using AutoMapper;
using LeaveManagement.Application.Exceptions;
using LeaveManagement.Application.Interfaces.Email;
using LeaveManagement.Application.Interfaces.Identity;
using LeaveManagement.Application.Interfaces.Logging;
using LeaveManagement.Application.Interfaces.Persistence;
using LeaveManagement.Application.Models.Email;
using MediatR;

namespace LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;

public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, Guid>
{
    private readonly IEmailSender _emailSender;
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IAppLogger<CreateLeaveRequestCommandHandler> _logger;
    private readonly IUserService _userService;

    public CreateLeaveRequestCommandHandler(
        IEmailSender emailSender,
        IMapper mapper, 
        ILeaveTypeRepository leaveTypeRepository, 
        ILeaveRequestRepository leaveRequestRepository,
        IAppLogger<CreateLeaveRequestCommandHandler> logger, 
        IUserService userService, 
        ILeaveAllocationRepository leaveAllocationRepository)
    {
        _emailSender = emailSender;
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
        _leaveRequestRepository = leaveRequestRepository;
        _logger = logger;
        _userService = userService;
        _leaveAllocationRepository = leaveAllocationRepository;
    }

    public async Task<Guid> Handle(
        CreateLeaveRequestCommand request,
        CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveRequestCommandValidator(_leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(
                request, 
                cancellationToken);
        
        if (validationResult.Errors.Any())
        {
            var allErrors = string
                .Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));

            _logger.LogError(
                "Validation errors in create request for {0} - {1}. Errors: {2}", 
                nameof(Domain.LeaveRequest), 
                request.LeaveTypeId, 
                allErrors);
        
            throw new BadRequestException(
                "Invalid Leave Request", 
                validationResult);
        }
        
        var employeeId = _userService.UserId;

        var allocation = await _leaveAllocationRepository.GetUserAllocations(
                userId: employeeId,
                leaveTypeId: request.LeaveTypeId);

        if (allocation is null)
        {
            var validationFailure = new FluentValidation.Results.ValidationFailure(
                propertyName: nameof(request.LeaveTypeId),
                errorMessage: "You don't have any allocations for this leave request");
            
            validationResult.Errors.Add(validationFailure);
            
            throw new BadRequestException(
                "Invalid Leave Request", 
                validationResult);
        }
        
        int daysRequested = (int)(request.EndDate - request.StartDate).TotalDays;

        if (daysRequested > allocation.NumberOfDays)
        {
            var validationFailure = new FluentValidation.Results.ValidationFailure(
                propertyName: nameof(request.EndDate),
                errorMessage: "You don't have enough days for this leave request");
            
            validationResult.Errors.Add(validationFailure);
            
            throw new BadRequestException(
                "Invalid Leave Request", 
                validationResult);
        }
        
        var leaveRequest = _mapper.Map<Domain.LeaveRequest>(request);
        leaveRequest.RequestingEmployeeId = employeeId;
        leaveRequest.DateRequested = DateTime.UtcNow;
        
        await _leaveRequestRepository.CreateAsync(leaveRequest);
        
        try
        {
            var email = new EmailMessage
            {
                To = string.Empty,
                Body = $"Your leave request for {request.StartDate:D} to {request.EndDate:D} " +
                    $"has been submitted successfully.",
                Subject = "Leave Request Submitted"
            };

            await _emailSender.SendEmailAsync(email);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex.Message);
        }

        return leaveRequest.Id;
    }
}