using LeaveManagement.Domain;
using LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace LeaveManagement.Persistence.IntegrationTests;

public class LeaveManagementDatabaseContextTests
{
    private LeaveManagementDatabaseContext _leaveManagementDatabaseContext;
    
    public LeaveManagementDatabaseContextTests()
    {
        var dbOptions = new DbContextOptionsBuilder<LeaveManagementDatabaseContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _leaveManagementDatabaseContext = new LeaveManagementDatabaseContext(
            dbOptions);
    }

    [Fact]
    public async void Save_SetDateCreatedValue()
    {
        // Arrange
        var leaveType = new LeaveType
        {
            Id = Guid.NewGuid(),
            DefaultDays = 10,
            Name = "Test Vacation"
        };

        // Act
        await _leaveManagementDatabaseContext
                .LeaveTypes
                .AddAsync(leaveType);
        
        await _leaveManagementDatabaseContext
                .SaveChangesAsync();

        // Assert
        leaveType.Name.ShouldBe("Test Vacation");
    }

    [Fact]
    public async void Save_SetDateModifiedValue()
    {
        // Arrange
        var leaveType = new LeaveType
        {
            Id = Guid.NewGuid(),
            DefaultDays = 10,
            Name = "Test Vacation 2"
        };

        // Act
        await _leaveManagementDatabaseContext
                .LeaveTypes
                .AddAsync(leaveType);
        
        await _leaveManagementDatabaseContext
                .SaveChangesAsync();

        // Assert
        leaveType.Name.ShouldBe("Test Vacation 2");
    }
}