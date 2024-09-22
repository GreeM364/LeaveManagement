using LeaveManagement.Application.Interfaces.Persistence;
using LeaveManagement.Domain;
using Moq;

namespace LeaveManagement.Application.UnitTests.Mocks;

public class MockLeaveTypeRepository
{
    public static Mock<ILeaveTypeRepository> GetMockLeaveTypeRepository()
    {
        var leaveTypes = new List<LeaveType>
        {
            new LeaveType
            {
                Id = Guid.NewGuid(),
                DefaultDays = 10,
                Name = "Test Vacation"
            },
            new LeaveType
            {
                Id = Guid.NewGuid(),
                DefaultDays = 15,
                Name = "Test Sick"
            },
            new LeaveType
            {
                Id = Guid.NewGuid(),
                DefaultDays = 15,
                Name = "Test Maternity"
            }
        };
        
        var mockRepo = new Mock<ILeaveTypeRepository>();

        mockRepo
            .Setup(repository => repository.GetAsync())
            .ReturnsAsync(leaveTypes);

        mockRepo
            .Setup(r => r.CreateAsync(It.IsAny<LeaveType>()))
            .Returns((LeaveType leaveType) =>
            {
                leaveTypes.Add(leaveType);
                return Task.CompletedTask;
            });
            
        mockRepo
            .Setup(r => r.IsLeaveTypeUnique(It.IsAny<string>()))
            .ReturnsAsync((string name) => { 
                return leaveTypes.All(q => q.Name != name);
            });

        return mockRepo;
    }
}