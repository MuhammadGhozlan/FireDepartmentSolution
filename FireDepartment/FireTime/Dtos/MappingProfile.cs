using AutoMapper;
using FireTime.Dtos.Attendance;
using FireTime.Dtos.Employees;
using FireTime.Dtos.Leave;
using FireTime.Dtos.Lookups;
using FireTime.Models;

namespace FireTime.Dtos
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AttendanceRequest, Models.Attendance>().ForMember(dest => dest.AttendanceId, opt => opt.Ignore())
                                                             .ForMember(dest => dest.DeletedInd, opt => opt.Ignore());
            CreateMap<Models.Attendance, AttendanceResponse>();
            CreateMap<AttendanceStatus, AttendanceStatusResponse>();

            CreateMap<CreateEmployeeRequest, Employee>();
            CreateMap<Employee, EmployeeDetailResponse>();

            CreateMap<EmployeeAssignmentRequest, EmployeeAssignment>().ForMember(dest => dest.EmployeeAssignmentId, opt => opt.Ignore())
                                                                      .ForMember(dest => dest.DeletedInd, opt => opt.Ignore());

            CreateMap<Station, StationResponse>();
            CreateMap<Company, CompanyResponse>();
            CreateMap<WorkPeriod, WorkPeriodResponse>();
            CreateMap<Shift, ShiftResponse>();
            CreateMap<JobTitle, JobTitleResponse>();
            CreateMap<CompanyPosition, CompanyPositionResponse>();
            CreateMap<EmployeeStatusCode, EmployeeStatusCodeResponse>();


            CreateMap<LeaveRequestRequest, LeaveRequest>().ForMember(dest => dest.LeaveRequestId, opt => opt.Ignore());
            CreateMap<LeaveRequest, LeaveRequestResponse>().ForCtorParam("ApprovalStatusName", opt=>opt.MapFrom(src=>src.ApprovalStatus.ApprovalStatusCde));
        }
    }
}
