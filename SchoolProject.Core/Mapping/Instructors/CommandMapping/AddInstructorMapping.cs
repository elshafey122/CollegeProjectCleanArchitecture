using SchoolProject.Core.Features.Instructors.Commands.Models;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Instructors
{
    public partial class InstructorProfile
    {
        public void AddInstructorMapping()
        {
            CreateMap<AddInstructorCommand, Instructor>()
                .ForMember(des => des.NameAr, opt => opt.MapFrom(x => x.InstructotNameAr))
                .ForMember(des => des.NameEn, opt => opt.MapFrom(x => x.InstructorNameEn))
                .ForMember(des => des.DId, opt => opt.MapFrom(x => x.DepartementId));
        }
    }
}
