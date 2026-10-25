using FireTime.Dtos.Employees;
using FireTime.Interfaces.Service_Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FireTime.Controllers
{
    [Route("api/employeeAssignment")]
    [ApiController]
    public class EmployeeAssignmentController : ControllerBase
    {
        private readonly IEmployeeAssignmentService _service;

        public EmployeeAssignmentController(IEmployeeAssignmentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmployeeAssignmentResponse>>> GetEmployeeAssignments()
        {
            try
            {
                var result = await _service.GetEmployeeAssignments();
                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("employee/{roic}")]
        public async Task<ActionResult<List<EmployeeAssignmentResponse>>> GetEmployeeAssignmentsByEmployeeNbr(string roic)
        {
            try
            {
                var result = await _service.GetEmployeeAssignmentsByEmployeeNbr(roic);
                return Ok(result);
            }
            catch(ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{assignmentId}")]
        public async Task<ActionResult<EmployeeAssignmentResponse>> GetEmployeeAssignmentById([FromBody] EmployeeAssignmentRequest request, int assignmentId)
        {
            try
            {
                var result = await _service.GetEmployeeAssignmentById(request, assignmentId);
                return Ok(result);
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

        [HttpPost]
        public async Task<ActionResult<EmployeeAssignmentResponse>> CreateEmployeeAssignment([FromBody] EmployeeAssignmentRequest request)
        {
            try
            {
                var result = await _service.CreateEmployeeAssignment(request);
                return CreatedAtAction(nameof(GetEmployeeAssignmentById), new { assignmentId = result.EmployeeAssignmentId }, result);
            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{assignmentId}")]
        public async Task<ActionResult<EmployeeAssignmentResponse>> UpdateEmployeeAssignment([FromBody] EmployeeAssignmentRequest request, int assignmentId)
        {
            try
            {
                var result = await _service.UpdateEmployeeAssignment(request, assignmentId);
                return Ok(result);
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

        [HttpDelete("{assignmentId}")]
        public async Task<IActionResult> DeleteEmployeeAssignment([FromBody] EmployeeAssignmentRequest request, int assignmentId)
        {
            try
            {
                await _service.DeleteEmployeeAssignment(request, assignmentId);
                return NoContent();
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
