using LeaveManagement.UI.Interfaces;
using LeaveManagement.UI.Models.LeaveTypes;
using Microsoft.AspNetCore.Components;

namespace LeaveManagement.UI.Components.Pages.LeaveTypes;

public partial class Edit
{
    [Inject]
    ILeaveTypeService LeaveTypeService { get; set; }

    [Inject]
    NavigationManager _navManager { get; set; }

    [Parameter]
    public Guid id { get; set; }
    
    public string Message { get; private set; }
    
    LeaveTypeViewModel leaveType = new LeaveTypeViewModel();
    

    protected override async Task OnParametersSetAsync()
    {
        leaveType = await LeaveTypeService.GetLeaveTypeDetailsAsync(id);
    }

    protected async Task EditLeaveType()
    {
        var response = await LeaveTypeService.UpdateLeaveTypeAsync(id, leaveType);
        if (response.Success)
        {
            _navManager.NavigateTo("/leavetypes/");
        }
        
        Message = response.Message;
    }
}