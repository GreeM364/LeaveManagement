namespace LeaveManagement.Application.Exceptions;

public class NotFoundExceptions : Exception
{
    public NotFoundExceptions(string name, object key) 
        : base($"{name} ({key}) was not found")
    { }
}