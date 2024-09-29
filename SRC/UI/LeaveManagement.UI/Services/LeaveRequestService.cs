using LeaveManagement.UI.Interfaces;
using LeaveManagement.UI.Services.Base;

namespace LeaveManagement.UI.Services;

public class LeaveRequestService : BaseHttpService, ILeaveRequestService
{
    public LeaveRequestService(
        IClient client) : base(client)
    { }
}