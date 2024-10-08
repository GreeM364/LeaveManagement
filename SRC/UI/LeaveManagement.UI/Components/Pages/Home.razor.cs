﻿using LeaveManagement.UI.Interfaces;
using LeaveManagement.UI.Providers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace LeaveManagement.UI.Components.Pages;

public partial class Home
{
    [Inject]
    private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public IAuthenticationService AuthenticationService { get; set; }

    
    protected override async Task OnInitializedAsync()
    {
        await ((ApiAuthenticationStateProvider)
            AuthenticationStateProvider).GetAuthenticationStateAsync();
    }

    protected void GoToLogin()
    {
        NavigationManager.NavigateTo("login/");
    }

    protected void GoToRegister()
    {
        NavigationManager.NavigateTo("register/");
    }

    protected async void Logout()
    {
        await AuthenticationService.Logout();
    }
}