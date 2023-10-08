using SchoolProject.Core.Features.Students.Queries.Results;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Students
{
    public partial class StudentProfile
    {
        public void GetStudentListMapping()
        {
            CreateMap<Student, StudentListResponse>()
                .ForMember(x => x.DepartementName, opt => opt.MapFrom(src => src.Localize(src.Departement.DNameAr, src.Departement.DNameEn)))
                .ForMember(des => des.StuName, opt => opt.MapFrom(src => src.Localize(src.StuNameAr, src.StuNameEn)));
        }
    }
}
