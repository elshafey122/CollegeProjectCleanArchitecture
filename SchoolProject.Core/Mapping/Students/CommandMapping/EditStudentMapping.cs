using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Students
{
    public partial class StudentProfile
    {
        public void EditStudentMapping()
        {
            // source , destination
            CreateMap<EditStudentCommand, Student>()
           .ForMember(dest => dest.DID, opt => opt.MapFrom(src => src.DepartementId))
           .ForMember(dest => dest.StuId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.StuNameAr, opt => opt.MapFrom(src => src.StuNamear))
           .ForMember(dest => dest.StuNameEn, opt => opt.MapFrom(src => src.StuNameen));
        }
    }
}
