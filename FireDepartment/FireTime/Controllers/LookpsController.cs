using FireTime.Dtos.Lookups;
using FireTime.Interfaces.Service_Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FireTime.Controllers
{
    [Route("api/lookups")]
    [ApiController]
    public class LookpsController : ControllerBase
    {
        private readonly ILookupsService _lookupsService;

        public LookpsController(ILookupsService lookupsService)
        {
            _lookupsService = lookupsService;
        }

        [HttpGet("stations")]
        public async Task<ActionResult<List<StationResponse>>> GetAllStations()
        {
            try
            {
                var result = await _lookupsService.GetAllStations();
                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("companies")]
        public async Task<ActionResult<List<CompanyResponse>>> GetAllCompanies()
        {
            try
            {
                var result = await _lookupsService.GetAllCompanies();
                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("company-positions")]
        public async Task<ActionResult<List<CompanyPositionDetailResponse>>> GetAllCompanyPositions()
        {
            try
            {
                var result = await _lookupsService.GetAllCompanyPositions();
                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("shifts")]
        public async Task<ActionResult<List<ShiftResponse>>> GetAllShifts()
        {
            try
            {
                var result = await _lookupsService.GetAllShifts();
                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("job-titles")]
        public async Task<ActionResult<List<JobTitleResponse>>> GetAllJobTitles()
        {
            try
            {
                var result = await _lookupsService.GetAllJobTitles();
                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("work-periods")]
        public async Task<ActionResult<List<WorkPeriodResponse>>> GetWorkPeriods()
        {
            try
            {
                var result = await _lookupsService.GetWorkPeriods();
                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("employee-status-codes")]
        public async Task<ActionResult<List<EmployeeStatusCodeResponse>>> GetEmployeeStatusCodes()
        {
            try
            {
                var result = await _lookupsService.GetEmployeeStatusCodes();
                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
