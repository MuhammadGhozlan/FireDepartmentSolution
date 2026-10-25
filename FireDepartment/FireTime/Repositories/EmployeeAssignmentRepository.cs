using AutoMapper;
using FireTime.Dtos.Employees;
using FireTime.Interfaces.Repo_Interfaces;
using FireTime.Models;
using Microsoft.EntityFrameworkCore;

namespace FireTime.Repositories
{
    public class EmployeeAssignmentRepository : IEmployeeAssignmentRepository
    {
        private readonly IisFireTimeContext _context;
        private readonly IMapper _mapper;

        public EmployeeAssignmentRepository(IisFireTimeContext _context, IMapper _mapper)
        {
            this._context = _context;
            this._mapper = _mapper;
        }
        public async Task<EmployeeAssignmentResponse> CreateEmployeeAssignment(EmployeeAssignmentRequest request)
        {
            var employeeAssignment = _mapper.Map<EmployeeAssignment>(request);
            if(request.AssignmentEndDate.HasValue && request.AssignmentEndDate < request.AssignmentStartDate)
            {
                throw new InvalidOperationException("Assignment end date cannot be earlier than start date.");
            }

            employeeAssignment.Roic = request.Roic;
            employeeAssignment.CreatedBy = "System";
            employeeAssignment.CreatedDate = DateTime.Now;
            employeeAssignment.LastUpdateBy = "System";
            employeeAssignment.LastUpdateDate = DateTime.Now;

            await _context.EmployeeAssignments.AddAsync(employeeAssignment);
            await _context.SaveChangesAsync();

            return _mapper.Map<EmployeeAssignmentResponse>(employeeAssignment);
        }

        public async Task<bool> DeleteEmployeeAssignment(EmployeeAssignmentRequest request, int assignmentId)
        {
            var employeeAssignment = await _context.EmployeeAssignments
                                           .Where(ea => ea.Roic == request.Roic && ea.EmployeeAssignmentId == assignmentId && !ea.DeletedInd)
                                           .FirstOrDefaultAsync();

            if(employeeAssignment == null)
                throw new KeyNotFoundException($"No employee assignment found for ID {assignmentId} and ROIC {request.Roic}.");

            employeeAssignment.DeletedInd = true;
            employeeAssignment.LastUpdateBy = "System";
            employeeAssignment.LastUpdateDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }
      

        public async Task<EmployeeAssignmentResponse> GetEmployeeAssignmentById(string roic, int assignmentId)
        {
            var employeeAssignment = await _context.EmployeeAssignments
                                           .Where(ea => ea.Roic == roic && ea.EmployeeAssignmentId == assignmentId && !ea.DeletedInd)
                                           .FirstOrDefaultAsync();

            if(employeeAssignment == null)
                throw new KeyNotFoundException($"No employee assignment found for ID {assignmentId} and ROIC {roic}.");

            return _mapper.Map<EmployeeAssignmentResponse>(employeeAssignment);
        }

        public async Task<List<EmployeeAssignmentResponse>> GetEmployeeAssignments()
        {
            return await _context.EmployeeAssignments
                .Where(ea => !ea.DeletedInd)
                .Select(ea => new EmployeeAssignmentResponse(
                    ea.EmployeeAssignmentId,
                    ea.AssignmentStartDate,
                    ea.AssignmentEndDate,
                    ea.IsTemp,
                    ea.Roic,
                    ea.CompanyPositionId,
                    ea.WorkPeriodId,
                    _context.CompanyPositions
                        .Where(cp => cp.CompanyPositionId == ea.CompanyPositionId)
                        .Select(cp => cp.Company.CompanyNme)
                        .FirstOrDefault() ?? string.Empty,
                    _context.CompanyPositions
                        .Where(cp => cp.CompanyPositionId == ea.CompanyPositionId)
                        .Select(cp => cp.Company != null && cp.Company.Station != null ? cp.Company.Station.StationNbr : null)
                        .FirstOrDefault()
                ))
                .ToListAsync();
        }

        public async Task<List<EmployeeAssignmentResponse>> GetEmployeeAssignmentsByEmployeeNbr(string roic)
        {
            if(roic == null)
            {
                throw new ArgumentNullException(nameof(roic), " cannot pass null argument: roic");
            }
            var employeeAssignments = await _context.EmployeeAssignments.Where(ea => ea.Roic == roic && !ea.DeletedInd).ToListAsync();
            return _mapper.Map<List<EmployeeAssignmentResponse>>(employeeAssignments);
        }

        public async Task<EmployeeAssignmentResponse> UpdateEmployeeAssignment(EmployeeAssignmentRequest request, int assignmentId)
        {
            var employeeAssignment = await _context.EmployeeAssignments.Where(ea => ea.Roic == request.Roic && ea.EmployeeAssignmentId == assignmentId && !ea.DeletedInd).FirstOrDefaultAsync();
            if(employeeAssignment == null) throw new KeyNotFoundException($"no employee assignment for the given roic: {request.Roic}");
            if(request.AssignmentEndDate.HasValue && request.AssignmentEndDate < request.AssignmentStartDate)
            {
                throw new InvalidOperationException("Assignment end date cannot be earlier than start date.");
            }

            if(request.AssignmentStartDate != employeeAssignment.AssignmentStartDate)
            {
                employeeAssignment.AssignmentStartDate = request.AssignmentStartDate;
            }
            if(request.AssignmentEndDate != employeeAssignment.AssignmentEndDate)
            {
                employeeAssignment.AssignmentEndDate = request.AssignmentEndDate;
            }
            if(request.IsTemp != employeeAssignment.IsTemp)
            {
                employeeAssignment.IsTemp = request.IsTemp;
            }
            if(request.CompanyPositionId != employeeAssignment.CompanyPositionId)
            {
                employeeAssignment.CompanyPositionId = request.CompanyPositionId;
            }
            if(request.WorkPeriodId != employeeAssignment.WorkPeriodId)
            {
                employeeAssignment.WorkPeriodId = request.WorkPeriodId;
            }
            employeeAssignment.LastUpdateBy = "System";
            employeeAssignment.LastUpdateDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return _mapper.Map<EmployeeAssignmentResponse>(employeeAssignment);
        }
    }
}
