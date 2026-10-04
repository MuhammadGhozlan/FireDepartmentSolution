using FireTime.Dtos.Attendance;
using FireTime.Interfaces.RepoInterfaces;
using FireTime.Interfaces.ServiceInterfaces;
using FireTime.Models;

namespace FireTime.Services;

public sealed class AttendanceService : IAttendanceService
{
    private readonly IAttendanceRepository _repository;
    private readonly CrudService<Attendance, AttendanceRequest, AttendanceResponse, int> _crud;

    public AttendanceService(IAttendanceRepository repository, IHttpContextAccessor accessor)
    {
        _repository = repository;
        _crud = new(repository, accessor,
            request => new Attendance
            {
                AttendanceDate = request.AttendanceDate,
                Roic = request.Roic,
                EmployeeAssignmentId = request.EmployeeAssignmentId,
                AttendanceStatusId = request.AttendanceStatusId,
                AttendanceComments = request.AttendanceComments
            },
            (entity, request) =>
            {
                entity.AttendanceDate = request.AttendanceDate;
                entity.Roic = request.Roic;
                entity.EmployeeAssignmentId = request.EmployeeAssignmentId;
                entity.AttendanceStatusId = request.AttendanceStatusId;
                entity.AttendanceComments = request.AttendanceComments;
            },
            Map);
    }

    public bool SupportsDelete => _crud.SupportsDelete;
    public Task<AttendanceResponse?> GetAsync(int id, CancellationToken token) => _crud.GetAsync(id, token);
    public Task<IReadOnlyList<AttendanceResponse>> ListAsync(int offset, int limit, CancellationToken token) => _crud.ListAsync(offset, limit, token);
    public Task<bool> DeleteAsync(int id, CancellationToken token) => _crud.DeleteAsync(id, token);

    public async Task<IReadOnlyList<AttendanceResponse>> ForEmployeeAsync(string roic, DateOnly date, CancellationToken token) =>
        (await _repository.ForEmployeeAsync(roic, date, token)).Select(Map).ToArray();

    public async Task<AttendanceResponse> CreateAsync(AttendanceRequest request, CancellationToken token)
    {
        await ValidateAsync(request, token);
        return await _crud.CreateAsync(request, token);
    }

    public async Task<AttendanceResponse?> UpdateAsync(int id, AttendanceRequest request, CancellationToken token)
    {
        if (await _repository.FindAsync(id, token) is not { DeletedInd: false })
            return null;
        await ValidateAsync(request, token);
        return await _crud.UpdateAsync(id, request, token);
    }

    private async Task ValidateAsync(AttendanceRequest request, CancellationToken token)
    {
        if (string.IsNullOrWhiteSpace(request.Roic) || request.Roic.Length > 10)
            throw new ArgumentException("ROIC is required and must be at most 10 characters.");
        if (request.AttendanceComments?.Length > 1000)
            throw new ArgumentException("Attendance comments must be at most 1000 characters.");
        if (!await _repository.AssignmentMatchesAsync(request.EmployeeAssignmentId, request.Roic, request.AttendanceDate, token))
            throw new ArgumentException("The assignment does not belong to this employee on the attendance date.");
        if (!await _repository.StatusExistsAsync(request.AttendanceStatusId, token))
            throw new ArgumentException("Attendance status does not exist.");
    }

    private static AttendanceResponse Map(Attendance entity) =>
        new(entity.AttendanceId, entity.AttendanceDate, entity.Roic,
            entity.EmployeeAssignmentId, entity.AttendanceStatusId, entity.AttendanceComments);
}
