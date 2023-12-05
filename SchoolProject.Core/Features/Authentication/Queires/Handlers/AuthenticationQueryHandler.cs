using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Basics_Status;
using SchoolProject.Core.BasicsStatus;
using SchoolProject.Core.Features.Authentication.Queires.Modles;
using SchoolProject.Core.Localization;
using SchoolProject.Service.Abstractions;

namespace SchoolProject.Core.Features.Authentication.Queires.Handlers
{
    public class AuthenticationQueryHandler : ResponseHandler,
                                             IRequestHandler<AuthorizeUserTokenQuery, Response<string>>,
                                             IRequestHandler<ConfirmEmailQuery, Response<string>>,
                                             IRequestHandler<ConfirmResetPasswordQuery, Response<string>>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationQueryHandler(IStringLocalizer<SharedResources> stringLocalizer, IAuthenticationService authenticationService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _authenticationService = authenticationService;
        }

        public async Task<Response<string>> Handle(AuthorizeUserTokenQuery request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.ValidateToken(request.AccessToken);
            if (result == "NotExpired")
            {
                return Success(result);
            }
            return Unauthorized<string>(_stringLocalizer[SharedResourcesKeys.TokenIsExpired]);
        }

        public async Task<Response<string>> Handle(ConfirmEmailQuery request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.ConfirmEmail(request.UserId, request.Code);
            if (result == "FailedToConfirmEmail")
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToConfirmEmail]);

            return Success<string>(_stringLocalizer[SharedResourcesKeys.ConfirmEmailSuccessfully]);
        }

        public async Task<Response<string>> Handle(ConfirmResetPasswordQuery request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.ConfirmResetpassword(request.Code, request.Email);
            switch (result)
            {
                case ("UserNotFound"):
                    return NotFound<string>(_stringLocalizer[SharedResourcesKeys.UserNotFound]);
                case ("Failed"):
                    return Success<string>(_stringLocalizer[SharedResourcesKeys.InvalidCode]);
                case ("Success"):
                    return Success<string>("");
                default:
                    return BadRequest<string>(result);
            }
        }
    }
}
