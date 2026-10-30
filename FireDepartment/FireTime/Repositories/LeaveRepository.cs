using AutoMapper;
using FireTime.Dtos.Leave;
using FireTime.Interfaces.Repo_Interfaces;
using FireTime.Models;
using Microsoft.EntityFrameworkCore;

namespace FireTime.Repositories
{
    public class LeaveRepository : ILeaveRepository
    {
        private readonly IisFireTimeContext _context;
        private readonly IMapper _mapper;

        public LeaveRepository(IisFireTimeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> DeleteLeaveRequests(int leaveRequestId)
        {
            var leaveRequest = await _context.LeaveRequests.Where(lr => !lr.DeletedInd && lr.LeaveRequestId == leaveRequestId).FirstOrDefaultAsync();
            if(leaveRequest == null) throw new KeyNotFoundException($"there is no leave request available for this requestId {leaveRequestId}");
            leaveRequest.DeletedInd = true;
            leaveRequest.LastUpdateDate = DateTime.UtcNow;
            leaveRequest.LastUpdateBy = "System";
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<LeaveRequestResponse>> GetAllLeaveRequests() => _mapper.Map<List<LeaveRequestResponse>>(await _context.LeaveRequests.Include(lr => lr.ApprovalStatus).AsNoTracking().Where(lr => !lr.DeletedInd).ToListAsync());

        public async Task<List<LeaveRequestResponse>> GetAllLeaveRequestsForRequester(string requesterRoic) => _mapper.Map<List<LeaveRequestResponse>>(await _context.LeaveRequests.Include(lr => lr.ApprovalStatus).AsNoTracking().Where(lr => !lr.DeletedInd && lr.RequesterRoic == requesterRoic).ToListAsync());


        public async Task<LeaveRequestResponse> GetLeaveRequestById(int leaveRequestId)
        {
            var leaveRequest = await _context.LeaveRequests.Include(lr => lr.ApprovalStatus).AsNoTracking().FirstOrDefaultAsync(lr => lr.LeaveRequestId == leaveRequestId && !lr.DeletedInd);

            if(leaveRequest == null) throw new KeyNotFoundException($"there is no leave request available for this requestId {leaveRequestId}");
            return _mapper.Map<LeaveRequestResponse>(leaveRequest);
        }

        public async Task<LeaveRequestResponse> MakeLeaveRequests(LeaveRequestRequest request)
        {
            if(request == null) throw new ArgumentNullException(nameof(request), "the request body cannot be null");
            var leaveRequest = _mapper.Map<LeaveRequest>(request);

            leaveRequest.LeaveRequestSubmitDate = DateTime.UtcNow;
            leaveRequest.ApprovalStatusId = 1;
            leaveRequest.CreatedDate = DateTime.UtcNow;
            leaveRequest.CreatedBy = await _context.Employees.Where(e => e.Roic == request.RequesterRoic).Select(e => e.FirstNme + " " + e.LastNme).FirstOrDefaultAsync() ?? request.RequesterRoic;
            leaveRequest.LastUpdateDate = DateTime.UtcNow;
            leaveRequest.LastUpdateBy = "System";
            await _context.LeaveRequests.AddAsync(leaveRequest);

            await _context.SaveChangesAsync();

            var addedEntity = await _context.LeaveRequests.Include(lr => lr.ApprovalStatus).AsNoTracking().Where(lr => !lr.DeletedInd && lr.LeaveRequestId == leaveRequest.LeaveRequestId).FirstOrDefaultAsync();
            return _mapper.Map<LeaveRequestResponse>(addedEntity);
        }

        public async Task<LeaveRequestResponse> UpdateLeaveRequest(int leaveRequestId, LeaveRequestRequest request)
        {
            var leaveRequest = await _context.LeaveRequests.Where(lr => lr.LeaveRequestId == leaveRequestId && !lr.DeletedInd).FirstOrDefaultAsync();
            if(leaveRequest == null) throw new KeyNotFoundException($"no leave request could be found for {leaveRequestId}");
            _mapper.Map(request, leaveRequest);
            leaveRequest.LastUpdateDate = DateTime.UtcNow;
            leaveRequest.LastUpdateBy = request.RequesterRoic;
            await _context.SaveChangesAsync();

            var updatedEntity = await _context.LeaveRequests.Include(lr => lr.ApprovalStatus).AsNoTracking().Where(lr => !lr.DeletedInd && lr.LeaveRequestId == leaveRequestId).FirstOrDefaultAsync();
            return _mapper.Map<LeaveRequestResponse>(updatedEntity);
        }

    }
}
