using SchoolProject.Core.Features.ApplicationUser.Queires.ViewModel;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Mapping.ApplicationUser
{
    public partial class ApplicationUserProile
    {
        public void GetUserByIdMapper()
        {
            CreateMap<User, GetUserByIdResponse>();
        }
    }
}
