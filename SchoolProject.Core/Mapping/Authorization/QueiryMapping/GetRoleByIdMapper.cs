using SchoolProject.Core.Features.Authorization.Queires.ViewModel;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Mapping.Authorization
{
    public partial class AuthorizationProfile
    {
        public void GetRoleByIdMapper()
        {
            CreateMap<Role, GetRoleByIdViewMode>()
               .ForMember(des => des.RoleName, opt => opt.MapFrom(src => src.Name));
        }
    }
}
