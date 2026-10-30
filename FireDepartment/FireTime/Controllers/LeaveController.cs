using FireTime.Dtos.Leave;
using FireTime.Interfaces.Service_Interfaces;
using Microsoft.AspNetCore.Http;
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
            var result = await _leaveService.GetAllLeaveRequestsAsync();
            return Ok(result);
        }

        [HttpGet("GetAllLeaveRequestsForRequester/{requesterRoic}")]
        public async Task<ActionResult<List<LeaveRequestResponse>>> GetAllLeaveRequestsForRequester(string requesterRoic)
        {
            var result = await _leaveService.GetAllLeaveRequestsForRequesterAsync(requesterRoic);
            return Ok(result);
        }

        [HttpGet("GetLeaveRequestById/{leaveRequestId:int}")]
        public async Task<ActionResult<LeaveRequestResponse>> GetLeaveRequestById(int leaveRequestId)
        {
            var result = await _leaveService.GetLeaveRequestByIdAsync(leaveRequestId);
            return Ok(result);
        }

        [HttpPost("MakeLeaveRequest")]
        public async Task<ActionResult<LeaveRequestResponse>> MakeLeaveRequest([FromBody] LeaveRequestRequest request)
        {
            var result = await _leaveService.MakeLeaveRequestsAsync(request);
            return CreatedAtAction(nameof(GetLeaveRequestById), new { leaveRequestId = result.LeaveRequestId }, result);
        }

        [HttpPut("UpdateLeaveRequest/{leaveRequestId:int}")]
        public async Task<ActionResult<LeaveRequestResponse>> UpdateLeaveRequest(int leaveRequestId, [FromBody] LeaveRequestRequest request)
        {
            var result = await _leaveService.UpdateLeaveRequestAsync(leaveRequestId, request);
            return Ok(result);
        }

        [HttpDelete("DeleteLeaveRequest/{leaveRequestId:int}")]
        public async Task<ActionResult<bool>> DeleteLeaveRequest(int leaveRequestId)
        {
            var result = await _leaveService.DeleteLeaveRequestsAsync(leaveRequestId);
            return Ok(result);
        }
    }
}
