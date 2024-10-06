using AutoMapper;
using Blazored.LocalStorage;
using LeaveManagement.UI.Interfaces;
using LeaveManagement.UI.Models.LeaveTypes;
using LeaveManagement.UI.Services.Base;

namespace LeaveManagement.UI.Services;

public class LeaveTypeService : BaseHttpService, ILeaveTypeService
{
    private readonly IMapper _mapper;

    public LeaveTypeService(
        IClient client,
        ILocalStorageService localStorageService,
        IMapper mapper) 
            : base(
                client,
                localStorageService)
    {
        _mapper = mapper;
    }

    public async Task<List<LeaveTypeViewModel>> GetLeaveTypesAsync()
    {
        var leaveTypes = await _client.LeaveTypeAllAsync();
        var result = _mapper.Map<List<LeaveTypeViewModel>>(leaveTypes);

        return result;
    }

    public async Task<LeaveTypeViewModel> GetLeaveTypeDetailsAsync(Guid id)
    {
        var leaveType = await _client.LeaveTypeGETAsync(id);
        var result = _mapper.Map<LeaveTypeViewModel>(leaveType);

        return result;
    }

    public async Task<Response<Guid>> CreateLeaveTypeAsync(
        LeaveTypeViewModel leaveType)
    {
        try
        {
            var createLeaveTypeCommand = _mapper.Map<CreateLeaveTypeCommand>(leaveType);
            await _client.LeaveTypePOSTAsync(createLeaveTypeCommand);
            
            return new Response<Guid>()
            {
                Success = true,
            };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    public async Task<Response<Guid>> UpdateLeaveTypeAsync(
        Guid id, 
        LeaveTypeViewModel leaveType)
    {
        try
        {
            var updateLeaveTypeCommand = _mapper.Map<UpdateLeaveTypeCommand>(leaveType);
            await _client.LeaveTypePUTAsync(
                    id: id.ToString(), 
                    body: updateLeaveTypeCommand);
            
            return new Response<Guid>()
            {
                Success = true,
            };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    public async Task<Response<Guid>> DeleteLeaveTypeAsync(Guid id)
    {
        try
        {
            await _client.LeaveTypeDELETEAsync(id);
            
            return new Response<Guid>()
            {
                Success = true
            };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }
}