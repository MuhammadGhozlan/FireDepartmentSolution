using AutoMapper;
using FireTime.Dtos.Attendance;
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
        }
    }
}
