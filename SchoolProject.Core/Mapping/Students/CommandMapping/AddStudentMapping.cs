using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Students
{
    public partial class StudentProfile
    {
        public void AddStudentMapping()
        {
            // source , destination
            CreateMap<AddStudentCommand, Student>()
           .ForMember(dest => dest.DID, opt => opt.MapFrom(src => src.DepartementId))//destination , source
           .ForMember(dest => dest.StuNameAr, opt => opt.MapFrom(src => src.StuNamear))
           .ForMember(dest => dest.StuNameEn, opt => opt.MapFrom(src => src.StuNameen));

        }
    }
}
