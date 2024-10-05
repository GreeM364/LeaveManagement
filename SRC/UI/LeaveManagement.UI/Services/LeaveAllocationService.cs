using Blazored.LocalStorage;
using LeaveManagement.UI.Interfaces;
using LeaveManagement.UI.Services.Base;

namespace LeaveManagement.UI.Services;

public class LeaveAllocationService : BaseHttpService, ILeaveAllocationService
{
    public LeaveAllocationService(
        IClient client,
        ILocalStorageService localStorageService) 
            : base(
                client,
                localStorageService)
    { }
    
    public async Task<Response<Guid>> CreateLeaveAllocations(Guid leaveTypeId)
    {
        try
        {
            var response = new Response<Guid>();
            CreateLeaveAllocationCommand createLeaveAllocation = new()
            {
                LeaveTypeId = leaveTypeId
            };

            await _client.LeaveAllocationPOSTAsync(createLeaveAllocation);
            return response;
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }
}