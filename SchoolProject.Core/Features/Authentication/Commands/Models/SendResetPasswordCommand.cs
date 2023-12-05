using MediatR;
using SchoolProject.Core.Basics_Status;

namespace SchoolProject.Core.Features.Authentication.Commands.Models
{
    public class SendResetPasswordCommand : IRequest<Response<string>>
    {
        public string Email { get; set; }
    }
}
