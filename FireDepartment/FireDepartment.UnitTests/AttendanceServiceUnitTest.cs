using AutoMapper;
using FireTime.Dtos.Attendance;
using FireTime.Interfaces.Repo_Interfaces;
using FireTime.Models;
using FireTime.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace FireDepartment.UnitTests
{
    public class AttendanceServiceUnitTest
    {
        private readonly Mock<IAttendanceRepository> _mockAttendanceRepository;
        private readonly Mock<ILogger<AttendanceService>> _mockLogger;
        private readonly AttendanceService _attendanceService;
        public AttendanceServiceUnitTest()
        {
            _mockAttendanceRepository = new Mock<IAttendanceRepository>();
            _mockLogger = new Mock<ILogger<AttendanceService>>();
            _attendanceService = new AttendanceService(_mockAttendanceRepository.Object, _mockLogger.Object);
        }
        [Fact]
        public async Task GetAllAttendance_NoArgument_ReturnAllAttendances()
        {
            //Arrange
            var expectedResponse = new List<AttendanceResponse>
            {
               new AttendanceResponse
               (
                  AttendanceId: 391,
                  AttendanceDate: DateOnly.Parse("2026-10-07"),
                  Roic: "222222",
                  EmployeeAssignmentId: 4406,
                  AttendanceStatusId: 1,
                  AttendanceComments: "Absent",
                  DeletedInd: true
               ),
                   new AttendanceResponse
               (
                  AttendanceId: 390,
                  AttendanceDate: DateOnly.Parse("2026-10-06"),
                  Roic: "11111",
                  EmployeeAssignmentId: 4406,
                  AttendanceStatusId: 1,
                  AttendanceComments: "Present",
                  DeletedInd: true
               )

            };

             _mockAttendanceRepository.Setup(res => res.GetAllAttendance()).ReturnsAsync(expectedResponse);

            //Act
            var result = await _attendanceService.GetAllAttendance();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResponse, result);

        }
    }
}

