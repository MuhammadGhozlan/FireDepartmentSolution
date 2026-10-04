using FireTime.Dtos.Attendance;
using FireTime.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace FireTime.Controllers;

[Route("api/attendance")]
public sealed class AttendanceController(IAttendanceService service)
    : CrudController<AttendanceRequest, AttendanceResponse, int>(service)
{
    [HttpGet("getAttendance")]
    public async Task<ActionResult<IReadOnlyList<AttendanceResponse>>> GetAttendance(
        [FromQuery] string roic, [FromQuery] DateOnly date, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(roic))
            return BadRequest("ROIC is required.");
        return Ok(await service.ForEmployeeAsync(roic, date, cancellationToken));
    }

    [HttpPost("takeAttendance")]
    public async Task<ActionResult<AttendanceResponse>> TakeAttendance(
        AttendanceRequest request, CancellationToken cancellationToken)
    {
        try
        {
            return Ok(await service.CreateAsync(request, cancellationToken));
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
    }
}
