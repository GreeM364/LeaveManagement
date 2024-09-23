using LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using LeaveManagement.Application.Interfaces.Logging;
using LeaveManagement.Application.MappingProfiles;
using LeaveManagement.Application.UnitTests.Mocks;

namespace LeaveManagement.Application.UnitTests.Features.LeaveType.Commands;

public class CreateLeaveTypeCommandHandlerTests
{
    private readonly IMapper _mapper;
    private Mock<ILeaveTypeRepository> _mockRepo;
    private Mock<IAppLogger<CreateLeaveTypeCommandHandler>> _mockLogger;

    public CreateLeaveTypeCommandHandlerTests()
    {
        _mockRepo = MockLeaveTypeRepository.GetMockLeaveTypeRepository();

        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<LeaveTypeProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
        _mockLogger = new Mock<IAppLogger<CreateLeaveTypeCommandHandler>>();
    }
    
    [Fact]
    public async Task Handle_ValidLeaveType()
    {
        // Arrange
        var handler = new CreateLeaveTypeCommandHandler(
            mapper: _mapper,
            leaveTypeRepository: _mockRepo.Object,
            logger: _mockLogger.Object);

        var request = new CreateLeaveTypeCommand()
        {
            Name = "Test1",
            DefaultDays = 1
        };
        
        // Act
        await handler.Handle(request, CancellationToken.None);
        var leaveTypes = await _mockRepo.Object.GetAsync();
        
        // Assert
        leaveTypes.Count.ShouldBe(4);
    }
}