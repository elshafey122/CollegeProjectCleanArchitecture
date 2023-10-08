using SchoolProject.Core.Features.Students.Queries.Results;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Students
{
    public partial class StudentProfile
    {
        public void GetStudentByIdMapping()
        {
            CreateMap<Student, StudentbyIdresponse>()
                .ForMember(x => x.DepartementName, opt => opt.MapFrom(src => src.Localize(src.Departement.DNameAr, src.Departement.DNameEn)))
                .ForMember(x => x.StuName, opt => opt.MapFrom(src => src.Localize(src.StuNameAr, src.StuNameEn)));
        }
    }
}
