using MediatR;
using SchoolProject.Core.Basics_Status;
using SchoolProject.Data.Results;

namespace SchoolProject.Core.Features.Authentication.Commands.Models
{
    public class SignInCommand : IRequest<Response<JwtAuthResult>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }

    }
}
