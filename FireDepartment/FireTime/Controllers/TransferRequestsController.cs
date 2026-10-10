using FireTime.Dtos.Transfers;
using FireTime.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FireTime.Controllers;

[ApiController]
[Route("api/transfer-requests")]
[ServiceFilter(typeof(DevelopmentOnlyFilter))]
public sealed class TransferRequestsController(IRequestWorkflowService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<TransferRequestResponse>>> List(
        [FromQuery] int offset = 0, [FromQuery] int limit = 50, CancellationToken cancellationToken = default)
    {
        if (offset < 0 || limit is < 1 or > 200)
            return BadRequest("offset must be nonnegative and limit must be 1-200.");
        return Ok(await service.ListTransferAsync(offset, limit, cancellationToken));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TransferRequestResponse>> Get(int id, CancellationToken cancellationToken)
    {
        var result = await service.GetTransferAsync(id, cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost("{requesterRoic}")]
    public async Task<ActionResult<TransferRequestResponse>> Create(
        string requesterRoic, TransferRequestRequest request, CancellationToken cancellationToken)
    {
        try { return Ok(await service.CreateTransferAsync(requesterRoic, request, cancellationToken)); }
        catch (ArgumentException exception) { return BadRequest(exception.Message); }
        catch (DbUpdateException) { return Conflict("The record violates a database constraint."); }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<TransferRequestResponse>> Update(
        int id, TransferRequestRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await service.UpdateTransferAsync(id, request, cancellationToken);
            return result is null ? NotFound() : Ok(result);
        }
        catch (ArgumentException exception) { return BadRequest(exception.Message); }
        catch (InvalidOperationException exception) { return Conflict(exception.Message); }
        catch (DbUpdateException) { return Conflict("The record violates a database constraint."); }
    }
}
