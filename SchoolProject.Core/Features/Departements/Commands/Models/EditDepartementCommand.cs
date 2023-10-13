using MediatR;
using SchoolProject.Core.Basics_Status;

namespace SchoolProject.Core.Features.Departements.Commands.Models
{
    public class EditDepartementCommand : IRequest<Response<string>>
    {
        public int DId { get; set; }
        public string? DepartementNameAr { get; set; }
        public string? DepartementNameEn { get; set; }
        public int? InstructorManager { get; set; }
    }
}
