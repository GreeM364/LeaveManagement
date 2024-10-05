using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LeaveManagement.Application.Exceptions;
using LeaveManagement.Application.Interfaces.Identity;
using LeaveManagement.Application.Models.Identity;
using LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LeaveManagement.Identity.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly JwtSettings _jwtSettings;

    public AuthService(
        UserManager<ApplicationUser> userManager, 
        SignInManager<ApplicationUser> signInManager,
        IOptions<JwtSettings> jwtSettings)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<AuthResponse> LoginAsync(
        AuthRequest authRequest)
    {
        var user = await _userManager.FindByEmailAsync(authRequest.Email);

        if (user == null)
        {
            throw new NotFoundException(
                $"User with {authRequest.Email} not found.", authRequest.Email);
        }

        var result = await _signInManager.CheckPasswordSignInAsync(
                user: user, 
                password: authRequest.Password, 
                lockoutOnFailure: false);

        if (result.Succeeded == false)
        {
            throw new BadRequestException(
                $"Credentials for '{authRequest.Email} aren't valid'.");
        }

        JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

        var response = new AuthResponse
        {
            Id = user.Id,
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            Email = user.Email,
            UserName = user.UserName
        };

        return response;
    }
    
    private async Task<JwtSecurityToken> GenerateToken(
        ApplicationUser user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        var roleClaims = roles
                .Select(q => new Claim(ClaimTypes.Role, q))
                .ToList();

        var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id.ToString())
            }
            .Union(userClaims)
            .Union(roleClaims);

        var symmetricSecurityKey = 
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

        var signingCredentials = new SigningCredentials(
                key: symmetricSecurityKey, 
                algorithm: SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);
        
        return jwtSecurityToken;
    }

    public async Task<RegistrationResponse> RegisterAsync(
        RegistrationRequest registrationRequest)
    {
        var user = new ApplicationUser
        {
            Email = registrationRequest.Email,
            FirstName = registrationRequest.FirstName,
            LastName = registrationRequest.LastName,
            UserName = registrationRequest.UserName,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(
                user: user, 
                password: registrationRequest.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "Employee");
            
            var response = new RegistrationResponse()
            {
                UserId = user.Id
            };

            return response;
        }
        else
        {
            StringBuilder str = new StringBuilder();
            foreach (var err in result.Errors)
            {
                str.AppendFormat("•{0}\n", err.Description);
            }
                
            throw new BadRequestException($"{str}");
        }
    }
}