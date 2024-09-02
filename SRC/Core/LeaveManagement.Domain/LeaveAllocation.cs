using LeaveManagement.Domain.Common;

namespace LeaveManagement.Domain;

public class LeaveAllocation : BaseEntity
{
    public int NumberOfDays { get; set; }
    
    public LeaveType LeaveType { get; set; } = new ();
    public Guid LeaveTypeId { get; set; }
    public Guid EmployeeId { get; set; }
    
    public int Period { get; set; }
}