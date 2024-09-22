using LeaveManagement.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;
using LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval;
using LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;
using LeaveManagement.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest;
using LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;
using LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails;
using LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LeaveRequestsController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeaveRequestsController(
        IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(List<LeaveRequestsDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<LeaveRequestsDto>>> GetLeaveRequests()
    {
        var leaveRequests = await _mediator.Send(new GetLeaveRequestsQuery());
        
        return Ok(leaveRequests);
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(LeaveRequestDetailsDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<LeaveRequestDetailsDto>> GetLeaveRequestById(Guid id)
    {
        var query = new GetLeaveRequestDetailsQuery
        {
            Id = id
        };
        
        var leaveRequest = await _mediator.Send(query);
        
        return Ok(leaveRequest);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Post(CreateLeaveRequestCommand leaveRequest)
    {
        var response = await _mediator.Send(leaveRequest);
        
        return CreatedAtAction(
            actionName: nameof(GetLeaveRequestById), 
            routeValues: new { id = response },
            value: response);
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Put(UpdateLeaveRequestCommand leaveRequest)
    {
        await _mediator.Send(leaveRequest);
        
        return NoContent();
    }
    
    [HttpPut]
    [Route("CancelRequest")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> CancelRequest(CancelLeaveRequestCommand cancelLeaveRequest)
    {
        await _mediator.Send(cancelLeaveRequest);
        
        return NoContent();
    }
    
    [HttpPut]
    [Route("UpdateApproval")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> UpdateApproval(ChangeLeaveRequestApprovalCommand updateApprovalRequest)
    {
        await _mediator.Send(updateApprovalRequest);
        
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(Guid id)
    {
        var command = new DeleteLeaveRequestCommand
        {
            Id = id
        };
        
        await _mediator.Send(command);
        
        return NoContent();
    }
}