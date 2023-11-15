using MediatR;
using SchoolProject.Core.Basics_Status;

namespace SchoolProject.Core.Features.Authentication.Queires.Modles
{
    public class AuthorizeUserTokenQuery : IRequest<Response<string>>
    {
        public string AccessToken { get; set; }
    }
}
