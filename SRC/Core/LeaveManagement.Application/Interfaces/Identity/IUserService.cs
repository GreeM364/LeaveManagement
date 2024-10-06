using LeaveManagement.Application.Models.Identity;

namespace LeaveManagement.Application.Interfaces.Identity;

public interface IUserService
{
    Task<List<Employee>> GetEmployeesAsync();
    Task<Employee> GetEmployeeAsync(string employeeId);
    public Guid UserId { get; }
}