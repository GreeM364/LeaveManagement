﻿using LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using LeaveManagement.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation;
using LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;
using LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LeaveAllocationController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeaveAllocationController(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<LeaveAllocationDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<LeaveAllocationDto>>> GetLeaveAllocations(bool isLoggedInUser = false)
    {
        var leaveAllocations = await _mediator.Send(new GetLeaveAllocationListQuery());
        
        return Ok(leaveAllocations);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(LeaveAllocationDetailsDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<LeaveAllocationDetailsDto>> GetLeaveAllocationById(Guid id)
    {
        var query = new GetLeaveAllocationDetailsQuery()
        {
            Id = id
        };
        
        var leaveAllocationDetails = await _mediator.Send(query);
        
        return Ok(leaveAllocationDetails);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Post(CreateLeaveAllocationCommand leaveAllocation)
    {
        var response = await _mediator.Send(leaveAllocation);
        
        return CreatedAtAction(
            actionName: nameof(GetLeaveAllocationById), 
            routeValues: new { id = response },
            value: response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Put(UpdateLeaveAllocationCommand leaveAllocation)
    {
        await _mediator.Send(leaveAllocation);
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(Guid id)
    {
        var command = new DeleteLeaveAllocationCommand()
        {
            Id = id
        };
        
        await _mediator.Send(command);
        
        return NoContent();
    }
}