using SchoolProject.Core.Features.Departements.Commands.Models;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Departements
{
    public partial class DepartementProfile
    {
        public void AddDepartementMapper()
        {
            CreateMap<AddDepartementCommand, Departement>()
               .ForMember(des => des.InsManager, opt => opt.MapFrom(src => src.InstructorManager))
               .ForMember(des => des.DNameAr, opt => opt.MapFrom(src => src.DepartementNameAr))
               .ForMember(des => des.DNameEn, opt => opt.MapFrom(src => src.DepartementNameEn));
        }
    }
}
