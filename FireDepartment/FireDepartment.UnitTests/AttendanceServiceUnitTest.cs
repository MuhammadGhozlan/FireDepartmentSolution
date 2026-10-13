using AutoMapper;
using FireTime.Dtos.Attendance;
using FireTime.Models;
using FireTime.Repositories;
using Moq;
using MockQueryable.Moq;
using Microsoft.Extensions.Logging;

namespace FireDepartment.UnitTests
{
    public class AttendanceRepositoryUnitTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IisFireTimeContext> _mockContext;
        private readonly AttendanceRepository _attendanceRepository;
        public AttendanceRepositoryUnitTest()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(AttendanceRepository).Assembly, typeof(AttendanceResponse).Assembly);
            }, new LoggerFactory());

            _mapper = configuration.CreateMapper();
            _mockContext = new Mock<IisFireTimeContext>();
            _attendanceRepository = new AttendanceRepository(_mapper, _mockContext.Object);

        }

        [Fact]
        public async Task TakeAttendance_ValidEntry_200Response()
        {
            //Arrange
            var requestList = new List<AttendanceRequest>
            {
                new AttendanceRequest
                (
                    AttendanceDate:DateOnly.FromDateTime(DateTime.Now),
                    Roic:"100512",
                    AttendanceStatusId:1,
                    AttendanceComments:"Test Attendance"                    
                )
            };
            var employeeAssignments = new List<EmployeeAssignment>
            {
                new EmployeeAssignment
                {
                    EmployeeAssignmentId = 4406,
                    Roic = "100512",
                    DeletedInd = false,
                    AssignmentStartDate = DateOnly.FromDateTime(DateTime.Now),
                    AssignmentEndDate = null
                }
              
            };
            var responseList = new List<AttendanceResponse>
            {
                new AttendanceResponse
                (
                    AttendanceId:0,
                    AttendanceDate:DateOnly.FromDateTime(DateTime.Now),
                    Roic:"100512",
                    EmployeeAssignmentId:4406,
                    AttendanceStatusId:1,
                    AttendanceComments:"Test Attendance",
                    DeletedInd:false
                )
            };
            var attendenceEntities = new List<Attendance>
            {
                new Attendance
                {
                    AttendanceId=0,
                    AttendanceDate=DateOnly.FromDateTime(DateTime.Now),
                    Roic="100512",
                    EmployeeAssignmentId=1,
                    AttendanceStatusId=1,
                    AttendanceComments="Test Attendance",
                    DeletedInd=false
                }
            };
            _mockContext.Setup(context => context.Attendances).Returns(attendenceEntities.BuildMockDbSet().Object);
            _mockContext.Setup(context => context.EmployeeAssignments).Returns(employeeAssignments.BuildMockDbSet().Object); 
            _mockContext.Setup(x=>x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            //Act
            var actualResult = await _attendanceRepository.TakeAttendance(requestList);
            //Assert
            Assert.Equal(responseList, actualResult);
        }
    }
}