using MediatR;
using SchoolProject.Core.Basics_Status;

namespace SchoolProject.Core.Features.Departements.Commands.Models
{
    public class DeleteDepartementCommand : IRequest<Response<string>>
    {
        public int DId { get; set; }
    }
}
