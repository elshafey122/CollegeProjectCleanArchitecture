using SchoolProject.Core.Features.Authorization.Queires.ViewModel;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Mapping.Authorization
{
    public partial class AuthorizationProfile
    {
        public void GetRolesListMapping()
        {
            CreateMap<Role, GetRolesViewMode>()
               .ForMember(des => des.RoleName, opt => opt.MapFrom(src => src.Name));
        }
    }

}
