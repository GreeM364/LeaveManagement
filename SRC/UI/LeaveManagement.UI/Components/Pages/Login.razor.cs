using LeaveManagement.UI.Interfaces;
using LeaveManagement.UI.Models;
using Microsoft.AspNetCore.Components;

namespace LeaveManagement.UI.Components.Pages;

public partial class Login
{
    [Inject]
    private IAuthenticationService AuthenticationService { get; set; }
    
    [Inject]
    public NavigationManager NavigationManager { get; set; }
    
    public LoginViewModel Model { get; set; }
    public string Message { get; set; }
    
    
    public Login()
    { }

    protected override void OnInitialized()
    {
        Model = new LoginViewModel();
    }

    protected async Task HandleLogin()
    {
        if (await AuthenticationService.AuthenticateAsync(Model.Email, Model.Password))
        {
            NavigationManager.NavigateTo("/");
        }
        
        Message = "Username/password combination unknown";
    }
}