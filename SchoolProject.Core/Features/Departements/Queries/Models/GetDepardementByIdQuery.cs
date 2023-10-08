using MediatR;
using SchoolProject.Core.Basics_Status;
using SchoolProject.Core.Features.Departements.Queries.ViewModels;

namespace SchoolProject.Core.Features.Departements.Queries.Models
{
    public class GetDepardementByIdQuery : IRequest<Response<DepartementByIdResponse>>
    {
        public int Id { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
