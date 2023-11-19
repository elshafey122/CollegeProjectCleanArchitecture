using MediatR;
using SchoolProject.Core.Basics_Status;
using SchoolProject.Data.Results;

namespace SchoolProject.Core.Features.Authorization.Queires.Model
{
    public class ManageUserClaimsQuery : IRequest<Response<ManageUserClaimsResult>>
    {
        public int UserId { get; set; }
    }
}
