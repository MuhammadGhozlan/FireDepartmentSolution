using FireTime.Dtos.Lookups;

namespace FireTime.Interfaces.Repo_Interfaces
{
    public interface ILookupsRepository
    {
        Task<List<StationResponse>> GetAllStations();
        Task<List<CompanyResponse>> GetAllCompanies();
        Task<List<CompanyPositionResponse>> GetAllCompanyPositions();
        Task<List<ShiftResponse>> GetAllShifts();
        Task<List<JobTitleResponse>> GetAllJobTitles();
        Task<List<WorkPeriodResponse>> GetWorkPeriods();
    }
}
