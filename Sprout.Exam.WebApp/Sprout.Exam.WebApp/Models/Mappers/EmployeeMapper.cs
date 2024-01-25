using AutoMapper;
using Sprout.Exam.Common.Enums;

namespace Sprout.Exam.WebApp.Models.Mapper
{
    public class EmployeeMapper : Profile
    {
        public EmployeeMapper()
        {
            CreateMap<Employee, EmployeeModel>()
                       .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                       .ForMember(d => d.FullName, o => o.MapFrom(s => s.FullName))
                       .ForMember(d => d.Birthdate, o => o.MapFrom(s => s.Birthdate))
                       .ForMember(d => d.EmployeeTypeId, o => o.MapFrom(s => s.EmployeeTypeId))
                       .ForMember(d => d.Tin, o => o.MapFrom(s => s.Tin))
                       .ForMember(d => d.EmployeeType, o => o.MapFrom(s => ((EmployeeType)s.EmployeeTypeId).ToString()))
                       .ForMember(d => d.IsDeleted, o => o.MapFrom(s => s.IsDeleted))
                       .ReverseMap();

        }
    }
}
