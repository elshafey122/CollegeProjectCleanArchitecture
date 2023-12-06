using MediatR;
using SchoolProject.Core.Basics_Status;
using SchoolProject.Core.Features.Departements.Queries.ViewModels;

namespace SchoolProject.Core.Features.Departements.Queries.Models
{
    public class GetDepartementStudentCountByIdQuery : IRequest<Response<GetDepartementStudentCountByIdResponse>>
    {
        public int DId { get; set; }
    }
}
