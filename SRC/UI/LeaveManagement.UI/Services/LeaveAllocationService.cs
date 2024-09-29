﻿using LeaveManagement.UI.Interfaces;
using LeaveManagement.UI.Services.Base;

namespace LeaveManagement.UI.Services;

public class LeaveAllocationService : BaseHttpService, ILeaveAllocationService
{
    public LeaveAllocationService(
        IClient client) : base(client)
    { }
}