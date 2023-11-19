using MediatR;
using SchoolProject.Core.Basics_Status;
using SchoolProject.Data.Results;

namespace SchoolProject.Core.Features.Authorization.Queires.Model
{
    public class ManageUserRolesQuery : IRequest<Response<ManageUserRolesResult>>
    {
        public int UserId { get; set; }
    }
}
