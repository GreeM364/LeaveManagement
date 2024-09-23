using LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using LeaveManagement.Application.Interfaces.Logging;
using LeaveManagement.Application.MappingProfiles;
using LeaveManagement.Application.UnitTests.Mocks;

namespace LeaveManagement.Application.UnitTests.Features.LeaveType.Queries;

public class GetLeaveTypeListQueryHandlerTests
{
    private readonly Mock<ILeaveTypeRepository> _mockRepo;
    private IMapper _mapper;
    private Mock<IAppLogger<GetLeaveTypesQueryHandler>> _mockLogger;

    public GetLeaveTypeListQueryHandlerTests()
    {
        _mockRepo = MockLeaveTypeRepository.GetMockLeaveTypeRepository();

        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<LeaveTypeProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
        _mockLogger = new Mock<IAppLogger<GetLeaveTypesQueryHandler>>();
    }
    
    [Fact]
    public async Task GetLeaveTypeListTest()
    {
        // Arrange
        var handler = new GetLeaveTypesQueryHandler(
                mapper: _mapper, 
                leaveTypeRepository: _mockRepo.Object, 
                logger: _mockLogger.Object);

        // Act
        var result = await handler.Handle(
                request: new GetLeaveTypesQuery(), 
                cancellationToken: CancellationToken.None);

        // Assert
        result.ShouldBeOfType<List<LeaveTypeDto>>();
        result.Count.ShouldBe(3);
    }
}