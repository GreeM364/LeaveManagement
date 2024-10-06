using LeaveManagement.UI.Interfaces;
using LeaveManagement.UI.Models.LeaveRequests;
using LeaveManagement.UI.Models.LeaveTypes;
using Microsoft.AspNetCore.Components;

namespace LeaveManagement.UI.Components.Pages.LeaveRequests;

public partial class Create
{
    [Inject] ILeaveTypeService leaveTypeService { get; set; }
    [Inject] ILeaveRequestService leaveRequestService { get; set; }
    [Inject] NavigationManager NavigationManager { get; set; }
    LeaveRequestViewModel LeaveRequest { get; set; } = new();
    List<LeaveTypeViewModel> LeaveTypeViewModels { get; set; } = new ();
    
    
    protected override async Task OnInitializedAsync()
    {
        LeaveTypeViewModels = await leaveTypeService.GetLeaveTypesAsync();
    }

    private async Task HandleValidSubmit()
    {
        NavigationManager.NavigateTo("/leaverequests/");
    }
}