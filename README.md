## ASP.NET Core using CQRS, MediatoR and Clean Architecture

# Application Architecture

The project is organized using the Clean Architecture principles. The solution is composed of the following projects:

- **LeaveManagement.Application**: Contains the application layer of the project, which contains the application logic and interfaces.
- **LeaveManagement.Domain**: Contains the domain layer of the project, which contains the domain models and business rules.
- **LeaveManagement.Infrastructure**: Contains the infrastructure layer of the project, which contains the implementation of the interfaces defined in the application layer.
- **LeaveManagement.Api**: Contains the API layer of the project.
- **LeaveManagement.BlazorUI**: Contains the Blazor client application.

![OnionArchitecture](https://github.com/user-attachments/assets/a1d5f1ca-dab8-4925-a865-fdebeb8003ce)
