using AutoMapper;
using LeaveManagmentSystem2.web.Data;

namespace LeaveManagmentSystem2.web.MappingProfiles
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Data.LeaveType, Models.LeaveTypes.LeaveTypeReadOnlyVM>();
            CreateMap<Models.LeaveTypes.LeaveTypeCreateVM, Data.LeaveType>();
            CreateMap<Data.LeaveType, Models.LeaveTypes.LeaveTypeEditVM>().ReverseMap();
            CreateMap<Data.LeaveType, Models.LeaveTypes.LeaveTypeDeleteVM>().ReverseMap(); 
        }
    }
}
