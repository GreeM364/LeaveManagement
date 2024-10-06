using System.Security.Claims;
using LeaveManagement.Application.Interfaces.Identity;
using LeaveManagement.Application.Models.Identity;
using LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace LeaveManagement.Identity.Services;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(
        UserManager<ApplicationUser> userManager,
        IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public Guid UserId 
    { 
        get => Guid.Parse(_httpContextAccessor.HttpContext?.User
            .FindFirstValue("uid") ?? string.Empty); 
    }

    public async Task<List<Employee>> GetEmployeesAsync()
    {
        var employees = await _userManager.GetUsersInRoleAsync("Employee");

        var result = employees.Select(e => new Employee
        {
            Id = e.Id,
            Email = e.Email,
            FirstName = e.FirstName,
            LastName = e.LastName
        }).ToList();
        
        return result;
    }

    public async Task<Employee> GetEmployeeAsync(
        string employeeId)
    {
        var employee = await _userManager.FindByIdAsync(employeeId);
        
        var result = new Employee
        {
            Id = employee.Id,
            Email = employee.Email,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
        };
        
        return result;
    }
}