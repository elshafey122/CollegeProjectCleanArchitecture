using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Basics_Status;
using SchoolProject.Core.BasicsStatus;
using SchoolProject.Core.Features.Authentication.Queires.Modles;
using SchoolProject.Core.Localization;
using SchoolProject.Service.Abstractions;

namespace SchoolProject.Core.Features.Authentication.Queires.Handlers
{
    public class AuthorizeUserHandlerQuery : ResponseHandler, IRequestHandler<AuthorizeUserTokenQuery, Response<string>>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IAuthenticationService _authenticationService;
        public AuthorizeUserHandlerQuery(IStringLocalizer<SharedResources> stringLocalizer, IAuthenticationService authenticationService) : base(stringLocalizer)
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
    }
}
