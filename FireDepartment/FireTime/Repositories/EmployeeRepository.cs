using AutoMapper;
using FireTime.Dtos.Employees;
using FireTime.Interfaces.Repo_Interfaces;
using Microsoft.EntityFrameworkCore;
using FireTime.Models;

namespace FireTime.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IMapper _mapper;
        private readonly IisFireTimeContext _context;

        public EmployeeRepository(IMapper mapper, IisFireTimeContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<EmployeeDetailResponse> CreateEmployee(CreateEmployeeRequest employee)
        {
            var newEmployee = _mapper.Map<Employee>(employee);
            var exists = await _context.Employees.AnyAsync(e => e.Roic == employee.Roic || e.EmployeeNbr == employee.EmployeeNbr);
            if(exists) throw new InvalidOperationException($"Employee with ROIC {employee.Roic} or Employee Number {employee.EmployeeNbr} already exists.");
            await _context.Employees.AddAsync(newEmployee);
            await _context.SaveChangesAsync();
            return _mapper.Map<EmployeeDetailResponse>(newEmployee);
        }

        public async Task<EmployeeDetailResponse?> GetEmployeeByRoic(string roic)
        {
            var employeesList = await _context.Employees.Where(e => e.Roic == roic).FirstOrDefaultAsync();
            return _mapper.Map<EmployeeDetailResponse>(employeesList);
        }

        public async Task<List<EmployeeDetailResponse>> GetEmployees()
        {
            return _mapper.Map<List<EmployeeDetailResponse>>(await _context.Employees.ToListAsync());
        }

        public async Task<EmployeeDetailResponse> UpdateEmployee(UpdateEmployeeRequest employee, string roic)
        {
            var query = await _context.Employees.Where(e => e.Roic == roic).FirstOrDefaultAsync();
            if(query == null) throw new KeyNotFoundException($"Employee with ROIC {roic} not found.");


            if(employee.Hazmat != query.Hazmat)
            {
                query.Hazmat = employee.Hazmat;
            }
            if(employee.Paramedic != query.Paramedic)
            {
                query.Paramedic = employee.Paramedic;
            }
            if(employee.EmployeeStatusCde != query.EmployeeStatusCde)
            {
                query.EmployeeStatusCde = employee.EmployeeStatusCde;
            }
            if(employee.FirstNme != query.FirstNme)
            {
                query.FirstNme = employee.FirstNme;
            }
            if(employee.MiddleInitialNme != query.MiddleInitialNme)
            {
                query.MiddleInitialNme = employee.MiddleInitialNme;
            }
            if(employee.LastNme != query.LastNme)
            {
                query.LastNme = employee.LastNme;
            }
            if(employee.Suffix != query.Suffix)
            {
                query.Suffix = employee.Suffix;
            }
            if(employee.BirthDate != query.BirthDate)
            {
                query.BirthDate = employee.BirthDate;
            }
            if(employee.MaritalStatusCde != query.MaritalStatusCde)
            {
                query.MaritalStatusCde = employee.MaritalStatusCde;
            }
            if(employee.LastUpdateBy != query.LastUpdateBy)
            {
                query.LastUpdateBy = employee.LastUpdateBy;
            }
            if(employee.LastUpdateDate != query.LastUpdateDate)
            {
                query.LastUpdateDate = employee.LastUpdateDate;
            }
            await _context.SaveChangesAsync();
            var result = _mapper.Map<EmployeeDetailResponse>(query);

            return result;
        }
    }
}
