using Blazored.LocalStorage;
using LeaveManagement.UI.Interfaces;
using LeaveManagement.UI.Providers;
using LeaveManagement.UI.Services.Base;
using Microsoft.AspNetCore.Components.Authorization;

namespace LeaveManagement.UI.Services;

public class AuthenticationService : BaseHttpService, IAuthenticationService
{
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    
    public AuthenticationService(
        IClient client,
        ILocalStorageService localStorageService,
        AuthenticationStateProvider authenticationStateProvider) 
            : base(
                client,
                localStorageService)
    {
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<bool> AuthenticateAsync(
        string email, 
        string password)
    {
        try
        {
            AuthRequest authenticationRequest = new AuthRequest()
            {
                Email = email,
                Password = password
            };
            
            var authenticationResponse = await _client.LoginAsync(authenticationRequest);
            
            if (authenticationResponse.Token != string.Empty)
            {
                await _localStorageService.SetItemAsync(
                    key: "token", 
                    data: authenticationResponse.Token);

                await ((ApiAuthenticationStateProvider)
                    _authenticationStateProvider).LoggedIn();
                
                return true;
            }
            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> RegisterAsync(
        string firstName, 
        string lastName, 
        string userName, 
        string email, 
        string password)
    {
        RegistrationRequest registrationRequest = new RegistrationRequest()
        {
            FirstName = firstName, 
            LastName = lastName, 
            Email = email, 
            UserName = userName, 
            Password = password
        };
        
        var response = await _client.RegisterAsync(registrationRequest);

        if (!string.IsNullOrEmpty(response.UserId))
        {
            return true;
        }
        return false;
    }

    public async Task Logout()
    {
        await ((ApiAuthenticationStateProvider)
            _authenticationStateProvider).LoggedOut();
    }
}