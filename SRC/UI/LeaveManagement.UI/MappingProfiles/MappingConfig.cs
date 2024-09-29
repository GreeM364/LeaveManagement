using LeaveManagement.UI.Models.LeaveTypes;
using LeaveManagement.UI.Services.Base;
using AutoMapper;

namespace LeaveManagement.UI.MappingProfiles;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<LeaveTypeDto, LeaveTypeViewModel>()
            .ReverseMap();
        CreateMap<LeaveTypeDetailsDto, LeaveTypeViewModel>()
            .ReverseMap();
        CreateMap<CreateLeaveTypeCommand, LeaveTypeViewModel>()
            .ReverseMap();
        CreateMap<UpdateLeaveTypeCommand, LeaveTypeViewModel>()
            .ReverseMap();
    }
}