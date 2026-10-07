using FireTime.Dtos.Attendance;
using FireTime.Interfaces.Repo_Interfaces;
using FireTime.Interfaces.Service_Interfaces;

namespace FireTime.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly ILogger<AttendanceService> _logger;

        public AttendanceService(IAttendanceRepository _attendanceRepository, ILogger<AttendanceService> _logger)
        {
            this._attendanceRepository = _attendanceRepository;
            this._logger = _logger;
        }
        public async Task<AttendanceResponse> DeleteAttendance(int id)
        {
            if(id <= 0)
            {
                throw new ArgumentException("Invalid attendance ID");
            }
            var deletedInstance = await _attendanceRepository.DeleteAttendance(id);
            if(deletedInstance == null)
            {
                _logger.LogError($"Attendance with ID {id} not found for deletion.");
                throw new Exception("Attendance not found");
            }
            return deletedInstance;
        }

        public async Task<List<AttendanceResponse>> FilterAttendance(AttendanceRequest attendanceRequest)
        {
            var filteredResults = await _attendanceRepository.FilterAttendance(attendanceRequest);
            if(filteredResults.Count == 0)
            {
                _logger.LogInformation("No attendance records found matching the filter criteria.");
            }
            return filteredResults;

        }

        public async Task<List<AttendanceResponse>> GetAllAttendance()
        {
            var attendancees = await _attendanceRepository.GetAllAttendance();
            if(attendancees.Count == 0)
            {
                _logger.LogInformation("No attendance records found.");
            }
            return attendancees;            
        }

        public async Task<List<AttendanceResponse>> TakeAttendance(List<AttendanceRequest> attendanceRequestList)
        {
            var attendancees = await _attendanceRepository.TakeAttendance(attendanceRequestList);
            if(attendancees.Count == 0)
            {
                _logger.LogInformation("No attendance records have been taken.");
            }
            return attendancees;            
        }

        public async Task<AttendanceResponse> UpdateAttendance(AttendanceRequest attendanceRequest, int id)
        {
            if(id <= 0)
            {
                _logger.LogError($"Invalid attendance ID: {id}");
                throw new ArgumentException("Invalid attendance ID");
            }
            var attendant = await _attendanceRepository.UpdateAttendance(attendanceRequest, id);
            if(attendant == null)
            {
                _logger.LogInformation("No attendance records found.");
                throw new KeyNotFoundException($"Attendance record with ID {id} not found."); 
            }
            return attendant;
        }
    }
}
