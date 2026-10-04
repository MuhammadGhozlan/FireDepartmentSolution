using FireTime.Dtos.Attendance;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FireTime.Controllers
{
    [Route("api/attendance")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        [HttpGet("getAttendance")]
        public Task<ActionResult<AttendanceResponse>> GetAttendance([FromBody] AttendanceRequest attendanceRequest)
        {
            // Implement logic to retrieve attendance by ID
            throw new NotImplementedException();
        }

        [HttpPost("takeAttendance")]
        public Task<ActionResult<AttendanceResponse>> TakeAttendance([FromBody] AttendanceRequest attendanceRequest)
        {
            // Implement logic to retrieve attendance by ID
            throw new NotImplementedException();
        }
    }
}
