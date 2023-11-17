using MediatR;
using SchoolProject.Core.Basics_Status;

namespace SchoolProject.Core.Features.Authorization.Commnands.Models
{
    public class AddRoleCommand : IRequest<Response<string>>
    {
        public string RoleName { get; set; }
    }
}
