using FireTime.Dtos.Attendance;
using FireTime.Dtos.Lookups;
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
        [HttpGet("getAttendanceStatuses")]
        public async Task<ActionResult<List<AttendanceStatusResponse>>> GetAttendanceStatuses()
        {
            return Ok(await _attendanceService.GetAttendanceStatuses());
        }
        [HttpGet("getAttendanceAssignments")]
        public async Task<ActionResult<List<AttendanceAssignmentResponse>>> GetAttendanceAssignments([FromQuery] string roic, [FromQuery] DateOnly date)
        {
            try
            {
                return Ok(await _attendanceService.GetAttendanceAssignments(roic, date));
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getAttendanceRoster")]
        public async Task<ActionResult<List<AttendanceRosterResponse>>> GetAttendanceRoster([FromQuery] DateOnly date, [FromQuery] int workPeriodNbr)
        {
            try
            {
                return Ok(await _attendanceService.GetAttendanceRoster(date, workPeriodNbr));
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("takeAttendance")]
        public async Task<ActionResult<List<AttendanceResponse>>> TakeAttendance([FromBody] List<AttendanceRequest> attendanceRequests)
        {
            try
            {
                return Ok(await _attendanceService.TakeAttendance(attendanceRequests));
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
