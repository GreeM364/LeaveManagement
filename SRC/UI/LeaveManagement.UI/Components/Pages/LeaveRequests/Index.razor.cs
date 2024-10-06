using LeaveManagement.UI.Interfaces;
using Microsoft.AspNetCore.Components;

namespace LeaveManagement.UI.Components.Pages.LeaveRequests;

public partial class Index
{
    [Inject] NavigationManager NavigationManager { get; set; }

    void GoToDetails(int id)
    {
        NavigationManager.NavigateTo($"/leaverequests/details/{id}");
    }
}