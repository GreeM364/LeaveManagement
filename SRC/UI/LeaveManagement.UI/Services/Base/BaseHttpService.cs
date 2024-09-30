using System.Net.Http.Headers;
using Blazored.LocalStorage;

namespace LeaveManagement.UI.Services.Base;

public class BaseHttpService
{
    protected IClient _client;
    protected readonly ILocalStorageService _localStorageService;

    public BaseHttpService(
        IClient client,
        ILocalStorageService localStorageService)
    {
        _client = client;
        _localStorageService = localStorageService;
    }
    
    protected Response<Guid> ConvertApiExceptions<Guid>(
        ApiException ex)
    {
        if (ex.StatusCode == 400)
        {
            return new Response<Guid>()
            {
                Message = "Invalid data was submitted", 
                ValidationErrors = ex.Response, 
                Success = false
            };
        }
        else if (ex.StatusCode == 404)
        {
            return new Response<Guid>()
            {
                Message = "The record was not found.", 
                Success = false
            };
        }
        else
        {
            return new Response<Guid>() 
            { 
                Message = "Something went wrong, please try again later.", 
                Success = false 
            };
        }
    }

    protected async Task AddBearerTokenAsync()
    {
        if (await _localStorageService.ContainKeyAsync("token"))
        {
            var token = await _localStorageService.GetItemAsync<string>("token");
            
            _client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                scheme: "Bearer", 
                parameter: token);
        }
    }
}