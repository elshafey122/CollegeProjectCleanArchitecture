using SchoolProject.Core.Features.Instructors.Commands.Models;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Instructors
{
    public partial class InstructorProfile
    {
        public void EditInstructorMapping()
        {
            CreateMap<EditInstructorCommand, Instructor>()
                .ForMember(des => des.InsId, opt => opt.MapFrom(src => src.InstructorId))
                .ForMember(des => des.NameAr, opt => opt.MapFrom(src => src.InstructotNameAr))
                .ForMember(des => des.NameEn, opt => opt.MapFrom(src => src.InstructorNameEn))
                .ForMember(des => des.DId, opt => opt.MapFrom(src => src.DepartementId))
                .ForMember(des => des.SupervisorId, opt => opt.MapFrom(src => src.InstructorId));
        }
    }
}

