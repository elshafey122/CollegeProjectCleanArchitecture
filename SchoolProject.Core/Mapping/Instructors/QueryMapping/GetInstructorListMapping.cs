using SchoolProject.Core.Features.Instructors.Queries.ViewModels;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Instructors
{
    public partial class InstructorProfile
    {
        public void GetInstructorListMapping()
        {
            CreateMap<Instructor, GetInstructorResponse>()
                .ForMember(des => des.InstructorId, opt => opt.MapFrom(src => src.InsId))
                .ForMember(des => des.InstructorName, opt => opt.MapFrom(src => src.Localize(src.NameAr, src.NameEn)))
                .ForMember(des => des.DepartementName, opt => opt.MapFrom(src => src.Departement.Localize(src.Departement.DNameAr, src.Departement.DNameEn)))
                .ForMember(des => des.SupervisorName, opt => opt.MapFrom(src => src.supervisor.Localize(src.supervisor.NameAr, src.supervisor.NameEn)));
        }
    }
}
