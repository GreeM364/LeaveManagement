using AutoMapper;
using LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using LeaveManagement.Domain;

namespace LeaveManagement.Application.MappingProfiles;

public class LeaveTypeProfile : Profile
{
    public LeaveTypeProfile()
    {
        CreateMap<LeaveTypeDto, LeaveType>()
            .ReverseMap();
        CreateMap<LeaveTypeDetailsDto, LeaveType>()
            .ReverseMap();
        CreateMap<CreateLeaveTypeCommand, LeaveType>()
            .ReverseMap();
        CreateMap<UpdateLeaveTypeCommand, LeaveType>()
            .ReverseMap();
    }
}