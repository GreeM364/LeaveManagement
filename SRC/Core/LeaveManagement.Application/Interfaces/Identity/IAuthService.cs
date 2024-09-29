using LeaveManagement.Application.Models.Identity;

namespace LeaveManagement.Application.Interfaces.Identity;

public interface IAuthService
{
    Task<AuthResponse> LoginAsync(AuthRequest authRequest);
    Task<RegistrationResponse> RegisterAsync(RegistrationRequest registrationRequest);
}