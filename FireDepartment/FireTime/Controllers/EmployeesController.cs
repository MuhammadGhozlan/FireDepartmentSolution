using FireTime.Dtos.Employees;
using FireTime.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FireTime.Controllers;

[ApiController]
[Route("api/employees")]
[ServiceFilter(typeof(DevelopmentOnlyFilter))]
public sealed class EmployeesController(IEmployeeService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<EmployeeSummaryResponse>>> List(
        [FromQuery] int offset = 0, [FromQuery] int limit = 50, CancellationToken cancellationToken = default)
    {
        if (offset < 0 || limit is < 1 or > 200)
            return BadRequest("offset must be nonnegative and limit must be 1-200.");
        return Ok(await service.ListAsync(offset, limit, cancellationToken));
    }

    [HttpGet("{roic}")]
    public async Task<ActionResult<EmployeeDetailResponse>> Get(string roic, CancellationToken cancellationToken)
    {
        var result = await service.GetAsync(roic, cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<EmployeeDetailResponse>> Create(CreateEmployeeRequest request, CancellationToken cancellationToken)
    {
        try { return Ok(await service.CreateAsync(request, cancellationToken)); }
        catch (ArgumentException exception) { return BadRequest(exception.Message); }
        catch (DbUpdateException) { return Conflict("The record violates a database constraint."); }
    }

    [HttpPut("{roic}")]
    public async Task<ActionResult<EmployeeDetailResponse>> Update(
        string roic, UpdateEmployeeRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await service.UpdateAsync(roic, request, cancellationToken);
            return result is null ? NotFound() : Ok(result);
        }
        catch (ArgumentException exception) { return BadRequest(exception.Message); }
        catch (DbUpdateException) { return Conflict("The record violates a database constraint."); }
    }
}
