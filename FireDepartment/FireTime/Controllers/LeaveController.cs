using FireTime.Dtos.Leave;
using FireTime.Interfaces.Service_Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FireTime.Controllers
{
    [Authorize(Policy = "RequireEmployee")]
    [ApiController]
    [Route("api/Leave")]
    public class LeaveController : ControllerBase
    {
        private readonly ILeaveService _leaveService;

        public LeaveController(ILeaveService leaveService)
        {
            _leaveService = leaveService;
        }

        [HttpGet("GetAllLeaveRequests")]
        public async Task<ActionResult<List<LeaveRequestResponse>>> GetAllLeaveRequests()
        {
            try
            {
                var result = await _leaveService.GetAllLeaveRequestsAsync();
                return Ok(result);
            }
            catch(Exception)
            {
                return StatusCode(500, "An unexpected error occurred while retrieving leave requests.");
            }
        }

        [HttpGet("GetAllLeaveRequestsForRequester/{requesterRoic}")]
        public async Task<ActionResult<List<LeaveRequestResponse>>> GetAllLeaveRequestsForRequester()
        {
            try
            {
                var roic = User.FindFirst("roic")?.Value;
                if(roic == null) throw new KeyNotFoundException($"roic has not been found {roic}");
                var result = await _leaveService.GetAllLeaveRequestsForRequesterAsync(roic);
                return Ok(result);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception)
            {
                return StatusCode(500, "An unexpected error occurred while retrieving leave requests.");
            }
        }

        [HttpGet("GetLeaveRequestById/{leaveRequestId:int}")]
        public async Task<ActionResult<LeaveRequestResponse>> GetLeaveRequestById(int leaveRequestId)
        {
            try
            {
                var result = await _leaveService.GetLeaveRequestByIdAsync(leaveRequestId);
                return Ok(result);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch(Exception)
            {
                return StatusCode(500, "An unexpected error occurred while retrieving the leave request.");
            }
        }

        [HttpPost("MakeLeaveRequest")]
        public async Task<ActionResult<LeaveRequestResponse>> MakeLeaveRequest([FromBody] LeaveRequestRequest request)
        {
            try
            {
                var roic = User.FindFirst("roic")?.Value;
                if(roic == null) throw new KeyNotFoundException($"roic has not been found {roic}");
                request = request with { RequesterRoic = roic };
                var result = await _leaveService.MakeLeaveRequestsAsync(request);
                return CreatedAtAction(nameof(GetLeaveRequestById), new { leaveRequestId = result.LeaveRequestId }, result);
            }
            catch(ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch(DbUpdateException)
            {
                return StatusCode(500, "A database error occurred while creating the leave request.");
            }
            catch(Exception)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPut("UpdateLeaveRequest/{leaveRequestId:int}")]
        public async Task<ActionResult<LeaveRequestResponse>> UpdateLeaveRequest(int leaveRequestId, [FromBody] LeaveRequestRequest request)
        {
            try
            {
                var result = await _leaveService.UpdateLeaveRequestAsync(leaveRequestId, request);
                return Ok(result);
            }
            catch(ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch(DbUpdateException)
            {
                return StatusCode(500, "A database error occurred while updating the leave request.");
            }
            catch(Exception)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpDelete("DeleteLeaveRequest/{leaveRequestId:int}")]
        public async Task<ActionResult<bool>> DeleteLeaveRequest(int leaveRequestId)
        {
            try
            {
                var result = await _leaveService.DeleteLeaveRequestsAsync(leaveRequestId);
                return Ok(result);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch(DbUpdateException)
            {
                return StatusCode(500, "A database error occurred while deleting the leave request.");
            }
            catch(Exception)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
        [HttpGet("GetMyClaims")]
        public IActionResult GetMyClaims()
        {
            return Ok(User.Claims.Select(c => new
            {
                c.Type,
                c.Value
            }));
        }
    }
}