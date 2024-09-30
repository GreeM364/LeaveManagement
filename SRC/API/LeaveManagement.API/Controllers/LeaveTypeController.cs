using LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;
using LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class LeaveTypeController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeaveTypeController(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<LeaveTypeDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<LeaveTypeDto>>> GetLeaveTypes()
    {
        var leaveTypes = await _mediator.Send(new GetLeaveTypesQuery());
        
        return Ok(leaveTypes);
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(LeaveTypeDetailsDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<LeaveTypeDetailsDto>> GetLeaveTypesById(Guid id)
    {
        var query = new GetLeaveTypeDetailsQuery
        {
            LeaveTypeId = id
        }; 
        
        var leaveTypeDetails = await _mediator.Send(query);
        
        return Ok(leaveTypeDetails);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Post(CreateLeaveTypeCommand leaveType)
    {
        var response = await _mediator.Send(leaveType);
        
        return CreatedAtAction(
            actionName: nameof(GetLeaveTypesById), 
            routeValues: new { id = response },
            value: response);
    }
    
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Put(UpdateLeaveTypeCommand leaveType)
    {
        await _mediator.Send(leaveType);
        
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(Guid id)
    {
        var command = new DeleteLeaveTypeCommand
        {
            Id = id
        };
        
        await _mediator.Send(command);
        
        return NoContent();
    }
}