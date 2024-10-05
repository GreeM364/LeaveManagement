using Microsoft.AspNetCore.Identity;

namespace LeaveManagement.Identity.Models;

public class ApplicationUser : IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}