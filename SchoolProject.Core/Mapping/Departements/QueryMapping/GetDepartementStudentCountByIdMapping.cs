using SchoolProject.Core.Features.Departements.Queries.Models;
using SchoolProject.Core.Features.Departements.Queries.ViewModels;
using SchoolProject.Data.Entities.Procedures;

namespace SchoolProject.Core.Mapping.Departements
{
    public partial class DepartementProfile
    {
        public void GetDepartementStudentCountByIdMapping()
        {
            CreateMap<GetDepartementStudentCountByIdQuery, DepartstudentContProcedeurParams>();

            CreateMap<DepartstudentContProcedeur, GetDepartementStudentCountByIdResponse>()
                .ForMember(des => des.DName, opt => opt.MapFrom(src => src.Localize(src.DNameAr, src.DNameEn)))
                .ForMember(des => des.StudentCount, opt => opt.MapFrom(src => src.StudentsCount));
        }
    }
}
