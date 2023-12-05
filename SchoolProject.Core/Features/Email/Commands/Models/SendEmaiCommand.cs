using MediatR;
using SchoolProject.Core.Basics_Status;

namespace SchoolProject.Core.Features.Email.Commands.Models
{
    public class SendEmaiCommand : IRequest<Response<string>>
    {
        public string Email { get; set; }
        public string Message { get; set; }
    }
}
