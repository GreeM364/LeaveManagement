using System.ComponentModel.DataAnnotations;
using LeaveManagement.UI.Models.LeaveTypes;

namespace LeaveManagement.UI.Models.LeaveRequests;

public class LeaveRequestViewModel
{
    public int Id { get; set; }

    [Display(Name = "Date Requested")]
    public DateTime DateRequested { get; set; }

    [Display(Name = "Date Actioned")]
    public DateTime DateActioned { get; set; }

    [Display(Name = "Approval State")]
    public bool? Approved { get; set; }

    public bool Cancelled { get; set; }
    
    public LeaveTypeViewModel LeaveType { get; set; } = new LeaveTypeViewModel();
    
    public EmployeeViewModel Employee { get; set; } = new EmployeeViewModel();

    [Display(Name = "Start Date")]
    [Required]
    public DateTime? StartDate { get; set; }

    [Display(Name = "End Date")]
    [Required]
    public DateTime? EndDate { get; set; }

    [Display(Name = "Leave Type")]
    [Required]
    public int LeaveTypeId { get; set; }

    [Display(Name = "Comments")]
    [MaxLength(300)]
    public string? RequestComments { get; set; }
}