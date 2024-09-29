using LeaveManagement.Application.Interfaces.Identity;
using LeaveManagement.Application.Models.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("login")]
    [ProducesResponseType(typeof(List<AuthResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AuthResponse>> Login(AuthRequest request)
    {
        var response = await _authService.LoginAsync(request);
        
        return Ok(response);
    }

    [HttpPost("register")]
    [ProducesResponseType(typeof(List<RegistrationResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest request)
    {
        var response = await _authService.RegisterAsync(request);
        
        return Ok(response);
    }
}