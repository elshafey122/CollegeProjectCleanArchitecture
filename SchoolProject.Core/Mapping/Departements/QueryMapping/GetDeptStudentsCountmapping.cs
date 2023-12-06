using SchoolProject.Core.Features.Departements.Queries.ViewModels;
using SchoolProject.Data.Entities.Views;

namespace SchoolProject.Core.Mapping.Departements
{
    public partial class DepartementProfile
    {
        public void GetDeptStudentsCountmapping()
        {
            CreateMap<ViewDepartStudentCount, GetDepartementStudentListCountResponse>()
                .ForMember(des => des.DName, opt => opt.MapFrom(src => src.Localize(src.DNameAr, src.DNameEn)))
                .ForMember(des => des.StudentCount, opt => opt.MapFrom(src => src.StudentsCount));
        }

    }

}
