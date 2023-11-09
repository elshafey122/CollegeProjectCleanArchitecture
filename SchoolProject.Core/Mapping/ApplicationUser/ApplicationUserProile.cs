using AutoMapper;

namespace SchoolProject.Core.Mapping.ApplicationUser
{
    public partial class ApplicationUserProile : Profile
    {
        public ApplicationUserProile()
        {
            AddUserMapper();
            GetUserByIdMapper();
            EditUserMapper();
        }
    }
}
