using AutoMapper;

namespace Assessment.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmpDTO>()
            .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.salary, opt => opt.MapFrom(src => src.Salary))
            .ForMember(dest => dest.department, opt => opt.MapFrom(src => src.departmentId));
        }
    }
}