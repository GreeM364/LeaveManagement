using LeaveManagement.UI.Interfaces;
using LeaveManagement.UI.Models;
using Microsoft.AspNetCore.Components;

namespace LeaveManagement.UI.Components.Pages;

public partial class Register
{
    [Inject]
    public NavigationManager NavigationManager { get; set; }
    
    [Inject]
    private IAuthenticationService AuthenticationService { get; set; }
    
    public string Message { get; set; }
    public RegisterViewModel Model { get; set; }

    
    protected override void OnInitialized()
    {
        Model = new RegisterViewModel();
    }

    protected async Task HandleRegister()
    {
        var result = await AuthenticationService.RegisterAsync(
            Model.FirstName, 
            Model.LastName, 
            Model.UserName, 
            Model.Email, 
            Model.Password);

        if (result)
        {
            NavigationManager.NavigateTo("/");
        }
        
        Message = "Something went wrong, please try again.";
    }
}