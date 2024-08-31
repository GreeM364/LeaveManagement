using FluentValidation.Results;

namespace LeaveManagement.Application.Exeptions;

public class BadRequestExceptions : Exception
{
    public List<string> ValidationErrors { get; }
    
    public BadRequestExceptions(
        string message) : base(message)
    { }

    public BadRequestExceptions(
        string message,
        ValidationResult validationResult) : base(message)
    {
        ValidationErrors = new ();

        foreach (var errors in validationResult.Errors)
        {
            ValidationErrors.Add(errors.ErrorMessage);
        }
    }
}