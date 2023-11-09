using MediatR;
using SchoolProject.Core.Basics_Status;
using SchoolProject.Core.Features.ApplicationUser.Queires.ViewModel;

namespace SchoolProject.Core.Features.ApplicationUser.Queires.Model
{
    public class GetUserByIdQuery : IRequest<Response<GetUserByIdResponse>>
    {
        public int Id { get; set; }
        public GetUserByIdQuery(int id)
        {
            Id = id;
        }
    }
}
