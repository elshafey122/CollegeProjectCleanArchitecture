using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Basics_Status;
using SchoolProject.Core.BasicsStatus;
using SchoolProject.Core.Features.Authentication.Commands.Models;
using SchoolProject.Core.Localization;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Results;
using SchoolProject.Service.Abstractions;

namespace SchoolProject.Core.Features.Authentication.Commands.Handler
{
    public class SignInHandler : ResponseHandler, IRequestHandler<SignInCommand, Response<JwtAuthResult>>
                                                , IRequestHandler<RefreshTokenCommand, Response<JwtAuthResult>>
                                                , IRequestHandler<SendResetPasswordCommand, Response<string>>
                                                , IRequestHandler<ResetPasswordCommand, Response<string>>

    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signinmanager;
        private readonly IAuthenticationService _authenticationService;

        public SignInHandler(IStringLocalizer<SharedResources> stringLocalizer,
            UserManager<User> userManager, IAuthenticationService authenticationService, SignInManager<User> signinmanager) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _userManager = userManager;
            _authenticationService = authenticationService;
            _signinmanager = signinmanager;
        }

        // generate token
        public async Task<Response<JwtAuthResult>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            // check first for username
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                //return Unauthorized<string>();
                return BadRequest<JwtAuthResult>(_stringLocalizer[SharedResourcesKeys.UserNotFound]);
            }
            // check for password of username
            var SignInManager = await _signinmanager.CheckPasswordSignInAsync(user, request.Password, false);

            if (!SignInManager.Succeeded)
            {
                //return Unauthorized<string>();
                return BadRequest<JwtAuthResult>(_stringLocalizer[SharedResourcesKeys.NotCorrectPassword]);
            }

            // confirm email check
            if (!user.EmailConfirmed)
            {
                return BadRequest<JwtAuthResult>(_stringLocalizer[SharedResourcesKeys.EmailNotConfirmed]);
            }

            var token = await _authenticationService.GetjwtToken(user);
            return Success(token);
        }

        public async Task<Response<JwtAuthResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var jwtToken = _authenticationService.ReadJwtToken(request.AccessToken); // first we convirt stringtoken to jwtsecuritytoken
            var UserIdAndExpireDate = await _authenticationService.ValidateTokenDetails(jwtToken, request.AccessToken, request.RefreshToken);  // second we validate token and refreshtoken to decide if i make refresh token or not

            switch (UserIdAndExpireDate)
            {
                case ("AlgorithmIsWrong", null):
                    return Unauthorized<JwtAuthResult>(_stringLocalizer[SharedResourcesKeys.AlgorithmIsWrong]);
                case ("TokenIsNotExpired", null):
                    return Unauthorized<JwtAuthResult>(_stringLocalizer[SharedResourcesKeys.TokenIsNotExpired]);
                case ("RefreshTokenIsNotFound", null):
                    return Unauthorized<JwtAuthResult>(_stringLocalizer[SharedResourcesKeys.RefreshTokenIsNotFound]);
                case ("RefreshTokenIsExpired", null):
                    return Unauthorized<JwtAuthResult>(_stringLocalizer[SharedResourcesKeys.RefreshTokenIsExpired]);
            }

            var (userId, expiryDate) = UserIdAndExpireDate;   // get user of token to reach to userRefreshToken
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return Unauthorized<JwtAuthResult>();
            }

            var result = await _authenticationService.GenerateRefreshToken(user, jwtToken, request.RefreshToken, expiryDate);  // finally i regenerate access token and refreshtoken is remain as it was
            return Success(result);
        }

        public async Task<Response<string>> Handle(SendResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.SendResetpasswordCode(request.Email);
            switch (result)
            {
                case ("UserNotFound"):
                    return NotFound<string>(_stringLocalizer[SharedResourcesKeys.UserNotFound]);
                case ("ErrrorInUpdateAsync"):
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.TryInAnotherTime]);
                case ("Success"):
                    return Success<string>("");
                case ("Failed"):
                    return Success<string>(_stringLocalizer[SharedResourcesKeys.TryInAnotherTime]);
                default:
                    return BadRequest<string>(result);
            }
        }

        public async Task<Response<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.Resetpassword(request.Email, request.Password);
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
