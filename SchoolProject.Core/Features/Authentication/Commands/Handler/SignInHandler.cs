using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Basics_Status;
using SchoolProject.Core.BasicsStatus;
using SchoolProject.Core.Features.Authentication.Commands.Models;
using SchoolProject.Core.Localization;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Service.Abstractions;

namespace SchoolProject.Core.Features.Authentication.Commands.Handler
{
    public class SignInHandler : ResponseHandler, IRequestHandler<SignInCommand, Response<string>>
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
        public async Task<Response<string>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            // check first for username
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                //return Unauthorized<string>();
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UserNameIsExist]);
            }
            // check for password of username
            var SignInManager = await _signinmanager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!SignInManager.Succeeded)
            {
                //return Unauthorized<string>();
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.NotCorrectPassword]);
            }
            var token = await _authenticationService.GetjwtToken(user);
            return Success(token);
        }
    }
}
