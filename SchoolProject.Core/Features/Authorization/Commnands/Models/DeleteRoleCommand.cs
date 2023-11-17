using MediatR;
using SchoolProject.Core.Basics_Status;

namespace SchoolProject.Core.Features.Authorization.Commnands.Models
{
    public class DeleteRoleCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
