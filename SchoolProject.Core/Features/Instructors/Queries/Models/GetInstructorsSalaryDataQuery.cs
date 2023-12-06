using MediatR;
using SchoolProject.Core.Basics_Status;
using SchoolProject.Core.Features.Instructors.Queries.ViewModels;

namespace SchoolProject.Core.Features.Instructors.Queries.Models
{
    public class GetInstructorsSalaryDataQuery : IRequest<Response<List<GetInstructorsSalaryDataResponse>>>
    {

    }
}
