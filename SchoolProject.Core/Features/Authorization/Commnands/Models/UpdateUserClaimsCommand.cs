using MediatR;
using SchoolProject.Core.Basics_Status;
using SchoolProject.Data.Requests;

namespace SchoolProject.Core.Features.Authorization.Commnands.Models
{
    public class UpdateUserClaimsCommand : UpdateUserClaimsRequest, IRequest<Response<string>>
    {

    }
}
