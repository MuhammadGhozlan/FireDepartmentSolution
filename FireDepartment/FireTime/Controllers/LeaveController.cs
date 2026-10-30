using FireTime.Dtos.Leave;
using FireTime.Interfaces.Service_Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FireTime.Controllers
{
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
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetAllLeaveRequestsForRequester/{requesterRoic}")]
        public async Task<ActionResult<List<LeaveRequestResponse>>> GetAllLeaveRequestsForRequester(string requesterRoic)
        {
            try
            {
                var result = await _leaveService.GetAllLeaveRequestsForRequesterAsync(requesterRoic);
                return Ok(result);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
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
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("MakeLeaveRequest")]
        public async Task<ActionResult<LeaveRequestResponse>> MakeLeaveRequest([FromBody] LeaveRequestRequest request)
        {
            try
            {
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
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
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
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
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
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}