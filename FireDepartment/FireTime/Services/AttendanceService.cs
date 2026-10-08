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
                throw new KeyNotFoundException($"Attendance with ID {id} not found.");
            }
            return deletedInstance;
        }

        public async Task<List<AttendanceResponse>> FilterAttendance(AttendanceFilterRequest attendanceFilterRequest)
        {
            var filteredResults = await _attendanceRepository.FilterAttendance(attendanceFilterRequest);
            if(filteredResults.Count == 0)
            {
                _logger.LogInformation("No attendance records found matching the filter criteria.");
            }
            return filteredResults;

        }

        public async Task<List<AttendanceResponse>> GetAllAttendance()
        {
            var attendees = await _attendanceRepository.GetAllAttendance();
            if(attendees.Count == 0)
            {
                _logger.LogInformation("No attendance records found.");
            }
            return attendees;            
        }

        public async Task<List<AttendanceResponse>> TakeAttendance(List<AttendanceRequest> attendanceRequestList)
        {
            var attendees = await _attendanceRepository.TakeAttendance(attendanceRequestList);
            if(attendees.Count == 0)
            {
                _logger.LogInformation("No attendance records have been taken.");
            }
            return attendees;            
        }

        public async Task<AttendanceResponse> UpdateAttendance(UpdateAttendanceRequest updateAttendanceRequest, int id)
        {
            if(id <= 0)
            {
                _logger.LogError($"Invalid attendance ID: {id}");
                throw new ArgumentException("Invalid attendance ID");
            }
            var attendant = await _attendanceRepository.UpdateAttendance(updateAttendanceRequest, id);
            if(attendant == null)
            {
                _logger.LogInformation("No attendance records found.");
                throw new KeyNotFoundException($"Attendance record with ID {id} not found."); 
            }
            return attendant;
        }
    }
}
