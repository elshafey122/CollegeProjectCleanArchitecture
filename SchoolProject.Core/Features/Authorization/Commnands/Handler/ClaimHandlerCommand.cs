

using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Basics_Status;
using SchoolProject.Core.BasicsStatus;
using SchoolProject.Core.Features.Authorization.Commnands.Models;
using SchoolProject.Core.Localization;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Service.Abstractions;

namespace SchoolProject.Core.Features.Authorization.Commnands.Handler
{
    public class ClaimHandlerCommand : ResponseHandler, IRequestHandler<UpdateUserClaimsCommand, Response<string>>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly RoleManager<Role> _roleManager;
        private readonly IAuthorizationService _authorizationService;
        public ClaimHandlerCommand(IStringLocalizer<SharedResources> stringLocalizer,
            RoleManager<Role> roleManager, IAuthorizationService authorizationService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _roleManager = roleManager;
            _authorizationService = authorizationService;
        }

        public async Task<Response<string>> Handle(UpdateUserClaimsCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.UpdateUserClaims(request);
            switch (result)
            {
                case "UserIsNull":
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UserNotFound]);
                    break;
                case "FailedToDeleteoldClaims":
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToDeleteoldClaims]);
                    break;
                case "FailedToAddNewRoles":
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToAddNewClaims]);
                    break;
                case "FailedToUpdateUserRoles":
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUpdateUserClaims]);
                    break;
            }
            return Success<string>(_stringLocalizer[SharedResourcesKeys.Success]);
        }
    }
}
