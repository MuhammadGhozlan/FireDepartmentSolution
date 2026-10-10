using FireTime.Dtos.Employees;
using FireTime.Interfaces.RepoInterfaces;
using FireTime.Interfaces.ServiceInterfaces;
using FireTime.Models;

namespace FireTime.Services;

public sealed class EmployeeService(IEmployeeRepository repository, IHttpContextAccessor accessor) : IEmployeeService
{
    private string Actor => accessor.HttpContext?.User.Identity?.Name ?? "local-development";

    public async Task<IReadOnlyList<EmployeeSummaryResponse>> ListAsync(int offset, int limit, CancellationToken token) =>
        (await repository.ListAsync(offset, limit, token))
            .Select(e => new EmployeeSummaryResponse(e.Roic, e.EmployeeNbr, e.FirstNme, e.LastNme, e.EmployeeStatusCde))
            .ToArray();

    public async Task<EmployeeDetailResponse?> GetAsync(string roic, CancellationToken token)
    {
        var employee = await repository.FindAsync(roic, token);
        return employee is null ? null : Map(employee);
    }

    public async Task<EmployeeDetailResponse> CreateAsync(CreateEmployeeRequest request, CancellationToken token)
    {
        if (string.IsNullOrWhiteSpace(request.Roic) || request.Roic.Length > 10)
            throw new ArgumentException("ROIC is required and must be at most 10 characters.");
        await ValidateAsync(request.FirstNme, request.LastNme, request.EmployeeStatusCde, token);
        if (await repository.FindAsync(request.Roic, token) is not null)
            throw new ArgumentException("ROIC already exists.");
        var employee = new Employee
        {
            Roic = request.Roic,
            EmployeeNbr = request.EmployeeNbr,
            FirstNme = request.FirstNme,
            MiddleInitialNme = request.MiddleInitialNme,
            LastNme = request.LastNme,
            Suffix = request.Suffix,
            Paramedic = request.Paramedic,
            Hazmat = request.Hazmat,
            EmployeeStatusCde = request.EmployeeStatusCde
        };
        repository.StampCreated(employee, Actor, DateTime.UtcNow);
        await repository.AddAsync(employee, token);
        await repository.SaveAsync(token);
        return Map(employee);
    }

    public async Task<EmployeeDetailResponse?> UpdateAsync(string roic, UpdateEmployeeRequest request, CancellationToken token)
    {
        var employee = await repository.FindAsync(roic, token);
        if (employee is null)
            return null;
        await ValidateAsync(request.FirstNme, request.LastNme, request.EmployeeStatusCde, token);
        employee.FirstNme = request.FirstNme;
        employee.MiddleInitialNme = request.MiddleInitialNme;
        employee.LastNme = request.LastNme;
        employee.Suffix = request.Suffix;
        employee.Paramedic = request.Paramedic;
        employee.Hazmat = request.Hazmat;
        employee.EmployeeStatusCde = request.EmployeeStatusCde;
        repository.StampUpdated(employee, Actor, DateTime.UtcNow);
        await repository.SaveAsync(token);
        return Map(employee);
    }

    private async Task ValidateAsync(string firstName, string lastName, string statusCode, CancellationToken token)
    {
        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("First and last names are required.");
        if (!await repository.StatusExistsAsync(statusCode, token))
            throw new ArgumentException("Employee status does not exist.");
    }

    private static EmployeeDetailResponse Map(Employee e) =>
        new(e.Roic, e.EmployeeNbr, e.FirstNme, e.MiddleInitialNme,
            e.LastNme, e.Suffix, e.Paramedic, e.Hazmat, e.EmployeeStatusCde);
}
