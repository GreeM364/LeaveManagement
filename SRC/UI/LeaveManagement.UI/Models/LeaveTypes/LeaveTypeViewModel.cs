using System.ComponentModel.DataAnnotations;

namespace LeaveManagement.UI.Models.LeaveTypes;

public class LeaveTypeViewModel
{
    public Guid Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    [Display(Name = "Default Number Of Days")]
    public int DefaultDays { get; set; }
}