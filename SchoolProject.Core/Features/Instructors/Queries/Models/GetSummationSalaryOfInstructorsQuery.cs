using MediatR;
using SchoolProject.Core.Basics_Status;

namespace SchoolProject.Core.Features.Instructors.Queries.Models
{
    public class GetSummationSalaryOfInstructorsQuery : IRequest<Response<Decimal>>
    {

    }
}
