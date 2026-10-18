namespace FireTime.Controllers
{
    using FireTime.Dtos.Employees;
    using FireTime.Interfaces.Service_Interfaces;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("getEmployees")]
        public async Task<ActionResult<List<EmployeeDetailResponse>>> GetEmployees()
        {
            return Ok(await _employeeService.GetEmployees());
        }

        [HttpGet("getEmployeeByRoic/{roic}")]
        public async Task<ActionResult<EmployeeDetailResponse>> GetEmployeeByRoic(string roic)
        {
            var employee = await _employeeService.GetEmployeeByRoic(roic);
            if(employee == null)
            {
                return NotFound($"Employee with ROIC {roic} was not found.");
            }
            return Ok(employee);
        }

        [HttpPost("createEmployee")]
        public async Task<ActionResult<EmployeeDetailResponse>> CreateEmployee([FromBody] CreateEmployeeRequest createEmployeeRequest)
        {
            try
            {
                var result = await _employeeService.CreateEmployee(createEmployeeRequest);
                return Ok(result);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("updateEmployee/{roic}")]
        public async Task<ActionResult<EmployeeDetailResponse>> UpdateEmployee([FromBody] UpdateEmployeeRequest updateEmployeeRequest, string roic)
        {
            try
            {
                var result = await _employeeService.UpdateEmployee(updateEmployeeRequest, roic);
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
            catch(InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}