using MediatR;
using SchoolProject.Core.Features.ApplicationUser.Queires.ViewModel;
using SchoolProject.Core.Wrappings;

namespace SchoolProject.Core.Features.ApplicationUser.Queires.NewFolder.Model
{
    public class GetUsersPaginatedQuery : IRequest<PaginatedResult<GetUserPaginatedResponse>>
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
