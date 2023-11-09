using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Mapping.ApplicationUser
{
    public partial class ApplicationUserProile
    {
        public void AddUserMapper()
        {
            CreateMap<AddUserCommand, User>()
               .ForMember(des => des.PhoneNumber, opt => opt.MapFrom(src => src.Phone));
        }
    }
}
