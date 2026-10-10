using FireTime.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FireTime.Controllers;

[ApiController]
[ServiceFilter(typeof(DevelopmentOnlyFilter))]
public abstract class CrudController<TRequest, TResponse, TKey>(
    ICrudService<TRequest, TResponse, TKey> service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<TResponse>>> List(
        [FromQuery] int offset = 0, [FromQuery] int limit = 50, CancellationToken cancellationToken = default)
    {
        if (offset < 0 || limit is < 1 or > 200)
            return BadRequest("offset must be nonnegative and limit must be 1-200.");
        return Ok(await service.ListAsync(offset, limit, cancellationToken));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TResponse>> GetById(TKey id, CancellationToken cancellationToken)
    {
        var result = await service.GetAsync(id, cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<TResponse>> Create(TRequest request, CancellationToken cancellationToken)
    {
        try
        {
            return Ok(await service.CreateAsync(request, cancellationToken));
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
        catch (DbUpdateException)
        {
            return Conflict("The record violates a database constraint.");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TResponse>> Update(TKey id, TRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await service.UpdateAsync(id, request, cancellationToken);
            return result is null ? NotFound() : Ok(result);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
        catch (DbUpdateException)
        {
            return Conflict("The record violates a database constraint.");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(TKey id, CancellationToken cancellationToken)
    {
        if (!service.SupportsDelete)
            return StatusCode(StatusCodes.Status405MethodNotAllowed);
        return await service.DeleteAsync(id, cancellationToken) ? NoContent() : NotFound();
    }
}
