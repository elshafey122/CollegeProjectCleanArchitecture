using MediatR;
using SchoolProject.Core.Basics_Status;
using SchoolProject.Core.Features.Departements.Queries.ViewModels;

namespace SchoolProject.Core.Features.Departements.Queries.Models
{
    public class GetDepartementStudentListCountQurery : IRequest<Response<List<GetDepartementStudentListCountResponse>>>
    {

    }
}
