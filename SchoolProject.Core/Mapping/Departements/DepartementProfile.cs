using AutoMapper;

namespace SchoolProject.Core.Mapping.Departements
{
    public partial class DepartementProfile : Profile
    {
        public DepartementProfile()
        {
            GetDepartementByIdMapping();
        }
    }
}
