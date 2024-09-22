using AutoMapper;
using LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;
using LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using LeaveManagement.Domain;

namespace LeaveManagement.Application.MappingProfiles;

public class LeaveAllocationProfile : Profile
{
    public LeaveAllocationProfile()
    {
        CreateMap<LeaveAllocation, LeaveAllocationDto>()
            .ReverseMap();
        CreateMap<LeaveAllocation, LeaveAllocationDetailsDto>()
            .ReverseMap();
        CreateMap<LeaveAllocation, CreateLeaveTypeCommand>()
            .ReverseMap();
        CreateMap<LeaveAllocation, UpdateLeaveTypeCommand>()
            .ReverseMap();
    }
}