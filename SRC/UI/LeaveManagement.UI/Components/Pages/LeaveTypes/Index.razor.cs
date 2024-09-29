using LeaveManagement.UI.Interfaces;
using LeaveManagement.UI.Models.LeaveTypes;
using Microsoft.AspNetCore.Components;

namespace LeaveManagement.UI.Components.Pages.LeaveTypes;

public partial class Index
{
    [Inject] 
    public NavigationManager NavigationManager { get; set; }

    [Inject] 
    public ILeaveTypeService LeaveTypeService { get; set; }

    public List<LeaveTypeViewModel> LeaveTypes { get; private set; }

    public string Message { get; set; } = string.Empty;

    
    protected void CreateLeaveType()
    {
        NavigationManager.NavigateTo("/leavetypes/create/");
    }

    protected void AllocateLeaveType(Guid id)
    {
    }

    protected void EditLeaveType(Guid id)
    {
        NavigationManager.NavigateTo($"/leavetypes/edit/{id}");
    }

    protected void DetailsLeaveType(Guid id)
    {
        NavigationManager.NavigateTo($"/leavetypes/details/{id}");
    }

    protected async Task DeleteLeaveType(Guid id)
    {
        var response = await LeaveTypeService.DeleteLeaveTypeAsync(id);
        if (response.Success)
        {
            StateHasChanged();
        }
        else
        {
            Message = response.Message;
        }
    }

    protected override async Task OnInitializedAsync()
    {
        LeaveTypes = await LeaveTypeService.GetLeaveTypesAsync();
    }
}