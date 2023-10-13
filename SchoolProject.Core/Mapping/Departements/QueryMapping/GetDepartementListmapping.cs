using SchoolProject.Core.Features.Departements.Queries.ViewModels;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Departements
{
    public partial class DepartementProfile
    {
        public void GetDepartementListmapping()
        {
            CreateMap<Departement, DepartementListResponse>()
                .ForMember(des => des.Id, opt => opt.MapFrom(src => src.DID))
                .ForMember(des => des.DepartementName, opt => opt.MapFrom(src => src.Localize(src.DNameAr, src.DNameEn)))
                .ForMember(des => des.InstructorManager, opt => opt.MapFrom(src => src.Localize(src.Instructor.NameAr, src.Instructor.NameEn)));
        }
    }
}
