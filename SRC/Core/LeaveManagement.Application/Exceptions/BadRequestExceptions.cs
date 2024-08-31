namespace LeaveManagement.Application.Exeptions;

public class BadRequestExceptions : Exception
{
    public BadRequestExceptions(string message) : base(message)
    { }
}