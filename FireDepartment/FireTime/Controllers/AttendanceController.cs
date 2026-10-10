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
            try
            {
                return Ok(await _attendanceService.TakeAttendance(attendanceRequests));
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            
        }
        [HttpPost("filterAttendance")]
        public async Task<ActionResult<List<AttendanceResponse>>> FilterAttendance([FromBody] AttendanceFilterRequest attendanceFilterRequest)
        {
            return Ok(await _attendanceService.FilterAttendance(attendanceFilterRequest));
        }
        [HttpPut("updateAttendance/{id}")]
        public async Task<ActionResult<AttendanceResponse>> UpdateAttendance([FromBody] UpdateAttendanceRequest updateAttendanceRequest, int id)
        {
            try
            {
                return Ok(await _attendanceService.UpdateAttendance(updateAttendanceRequest, id));
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpDelete("deleteAttendance/{id}")]
        public async Task<ActionResult<AttendanceResponse>> DeleteAttendance(int id)
        {
            try
            {
                return Ok(await _attendanceService.DeleteAttendance(id));
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
