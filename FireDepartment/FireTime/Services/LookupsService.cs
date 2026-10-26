using FireTime.Dtos.Lookups;
using FireTime.Interfaces.Repo_Interfaces;
using FireTime.Interfaces.Service_Interfaces;

namespace FireTime.Services
{
    public class LookupsService : ILookupsService
    {
        private readonly ILookupsRepository _lookupsRepository;

        public LookupsService(ILookupsRepository lookupsRepository)
        {
            _lookupsRepository = lookupsRepository;
        }

        public async Task<List<StationResponse>> GetAllStations() => await _lookupsRepository.GetAllStations();

        public async Task<List<CompanyResponse>> GetAllCompanies() => await _lookupsRepository.GetAllCompanies();

        public async Task<List<CompanyPositionDetailResponse>> GetAllCompanyPositions() => await _lookupsRepository.GetAllCompanyPositions();

        public async Task<List<ShiftResponse>> GetAllShifts() => await _lookupsRepository.GetAllShifts();

        public async Task<List<JobTitleResponse>> GetAllJobTitles() => await _lookupsRepository.GetAllJobTitles();

        public async Task<List<WorkPeriodResponse>> GetWorkPeriods() => await _lookupsRepository.GetWorkPeriods();
        public async Task<List<EmployeeStatusCodeResponse>> GetEmployeeStatusCodes() => await _lookupsRepository.GetEmployeeStatusCodes();
    }
}
}
