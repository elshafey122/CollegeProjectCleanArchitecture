using MediatR;
using SchoolProject.Core.Basics_Status;
using SchoolProject.Data.Dto;

namespace SchoolProject.Core.Features.Authorization.Commnands.Models
{
    public class UpdateUserRolesCommand : UpdateUserRolesRequest, IRequest<Response<string>>
    {
    }
}
