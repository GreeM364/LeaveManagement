using LeaveManagement.UI.Interfaces;
using LeaveManagement.UI.Models.LeaveTypes;
using Microsoft.AspNetCore.Components;

namespace LeaveManagement.UI.Components.Pages.LeaveTypes;

public partial class Details
{
    [Inject]
    ILeaveTypeService LeaveTypeService { get; set; }
    
    [Inject]
    NavigationManager NavigationManager { get; set; }

    [Parameter]
    public Guid id { get; set; }

    LeaveTypeViewModel leaveType = new ();

    
    protected override async Task OnParametersSetAsync()
    {
        leaveType = await LeaveTypeService.GetLeaveTypeDetailsAsync(id);
    }
    
    protected void GoBack()
    {
        NavigationManager.NavigateTo("/leavetypes/");
    }
}