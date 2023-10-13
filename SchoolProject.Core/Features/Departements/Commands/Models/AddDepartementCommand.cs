using MediatR;
using SchoolProject.Core.Basics_Status;

namespace SchoolProject.Core.Features.Departements.Commands.Models
{
    public class AddDepartementCommand : IRequest<Response<string>>
    {
        public string? DepartementNameAr { get; set; }
        public string? DepartementNameEn { get; set; }
        public int? InstructorManager { get; set; }
    }
}
