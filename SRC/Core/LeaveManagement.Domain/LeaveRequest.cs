﻿using System.ComponentModel.DataAnnotations.Schema;
using LeaveManagement.Domain.Common;

namespace LeaveManagement.Domain;

public class LeaveRequest : BaseEntity
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    [ForeignKey("LeaveTypeId")]
    public LeaveType? LeaveType { get; set; }
    public Guid LeaveTypeId { get; set; }
    
    public DateTime DateRequested { get; set; }
    public string? RequestComments { get; set; }
    
    public bool? Approved { get; set; }
    public bool Cancelled { get; set; }

    public Guid RequestingEmployeeId { get; set; }
}