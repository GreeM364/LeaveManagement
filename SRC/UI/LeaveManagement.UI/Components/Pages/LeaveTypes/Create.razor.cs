
using LeaveManagement.UI.Interfaces;
using LeaveManagement.UI.Models.LeaveTypes;
using Microsoft.AspNetCore.Components;

namespace LeaveManagement.UI.Components.Pages.LeaveTypes;

public partial class Create
{
    [Inject]
    NavigationManager NavigationManager { get; set; }
    
    [Inject]
    ILeaveTypeService LeaveTypeService { get; set; }
    
    public string Message { get; private set; }

    LeaveTypeViewModel leaveType = new ();
    
    protected async Task CreateLeaveType()
    {
        var response = await LeaveTypeService.CreateLeaveTypeAsync(leaveType);
        if(response.Success)
        {
            NavigationManager.NavigateTo("/leavetypes/");
        }
        
        Message = response.Message;
    }
}