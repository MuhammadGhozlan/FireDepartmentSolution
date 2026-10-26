using FireTime.Dtos.Lookups;

namespace FireTime.Interfaces.Service_Interfaces
{
    public interface ILookupsService
    {
        Task<List<StationResponse>> GetAllStations();
        Task<List<CompanyResponse>> GetAllCompanies();
        Task<List<CompanyPositionDetailResponse>> GetAllCompanyPositions();
        Task<List<ShiftResponse>> GetAllShifts();
        Task<List<JobTitleResponse>> GetAllJobTitles();
        Task<List<WorkPeriodResponse>> GetWorkPeriods();
        Task<List<EmployeeStatusCodeResponse>> GetEmployeeStatusCodes();
    }
}
