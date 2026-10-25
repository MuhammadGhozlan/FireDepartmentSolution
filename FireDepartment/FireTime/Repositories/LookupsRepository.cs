using AutoMapper;
using FireTime.Dtos.Lookups;
using FireTime.Interfaces.Repo_Interfaces;
using FireTime.Models;
using Microsoft.EntityFrameworkCore;

namespace FireTime.Repositories
{
    public class LookupsRepository : ILookupsRepository
    {
        private readonly IisFireTimeContext _context;
        private readonly IMapper _mapper;
        public LookupsRepository(IisFireTimeContext _context, IMapper _mapper)
        {
            this._context = _context;
            this._mapper = _mapper;
        }
        public async Task<List<CompanyResponse>> GetAllCompanies()
        {
            return _mapper.Map<List<CompanyResponse>> (await _context.Companies.AsNoTracking().OrderBy(c=>c.CompanyId).ToListAsync());            
        }

        public async Task<List<CompanyPositionResponse>> GetAllCompanyPositions()
        {
            return _mapper.Map<List<CompanyPositionResponse>>(await _context.CompanyPositions.AsNoTracking().OrderBy(cp=>cp.CompanyPositionId).ToListAsync());
        }

        public async Task<List<JobTitleResponse>> GetAllJobTitles()
        {
            return _mapper.Map<List<JobTitleResponse>>(await _context.JobTitles.AsNoTracking().OrderBy(jt=>jt.JobTitleId).ToListAsync());
        }

        public async Task<List<ShiftResponse>> GetAllShifts()
        {
            return _mapper.Map<List<ShiftResponse>>(await _context.Shifts.AsNoTracking().OrderBy(s=>s.ShiftId).ToListAsync());
        }

        public async Task<List<StationResponse>> GetAllStations()
        {
            return _mapper.Map<List<StationResponse>>(await _context.Stations.AsNoTracking().OrderBy(st => st.StationId).ToListAsync());
        }

        public async Task<List<WorkPeriodResponse>> GetWorkPeriods()
        {
            return _mapper.Map<List<WorkPeriodResponse>>(await _context.WorkPeriods.AsNoTracking().OrderBy(wp => wp.WorkPeriodId).ToListAsync());
        }
    }
}
