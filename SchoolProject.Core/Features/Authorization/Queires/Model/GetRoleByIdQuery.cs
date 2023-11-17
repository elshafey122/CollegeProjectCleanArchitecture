using MediatR;
using SchoolProject.Core.Basics_Status;
using SchoolProject.Core.Features.Authorization.Queires.ViewModel;

namespace SchoolProject.Core.Features.Authorization.Queires.Model
{
    public class GetRoleByIdQuery : IRequest<Response<GetRoleByIdViewMode>>
    {
        public int Id { get; set; }
    }
}
