using MediatR;
using SchoolProject.Core.Basics_Status;
using SchoolProject.Data.Results;

namespace SchoolProject.Core.Features.Authentication.Commands.Models
{
    public class RefreshTokenCommand : IRequest<Response<JwtAuthResult>>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
