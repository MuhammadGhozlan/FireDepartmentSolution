using FireTime.Dtos.Attendance;
using FireTime.Interfaces.Service_Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FireTime.Controllers
{
    [Route("api/attendance")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;
        public AttendanceController(IAttendanceService _attendanceService)
        {
           this._attendanceService = _attendanceService;
        }
        [HttpGet("getAttendance")]
        public async Task<ActionResult<List<AttendanceResponse>>> GetAttendance()
        {
            return Ok(await _attendanceService.GetAllAttendance());             
        }
        [HttpPost("takeAttendance")]
        public async Task<ActionResult<List<AttendanceResponse>>> TakeAttendance([FromBody] List<AttendanceRequest> attendanceRequests)              
        {
            return Ok(await _attendanceService.TakeAttendance(attendanceRequests));
        }
        [HttpPost("filterAttendance")]
        public async Task<ActionResult<List<AttendanceResponse>>> FilterAttendance([FromBody] AttendanceRequest attendanceRequest)
        {
            return Ok(await _attendanceService.FilterAttendance(attendanceRequest));
        }
        [HttpPut("updateAttendance/{id}")]
        public async Task<ActionResult<AttendanceResponse>> UpdateAttendance([FromBody] AttendanceRequest attendanceRequest, int id)
        {
            return Ok(await _attendanceService.UpdateAttendance(attendanceRequest, id));
        }
        [HttpDelete("deleteAttendance/{id}")]
        public async Task<ActionResult<AttendanceResponse>> DeleteAttendance(int id)
        {
            return Ok(await _attendanceService.DeleteAttendance(id));
        }
    }
}
