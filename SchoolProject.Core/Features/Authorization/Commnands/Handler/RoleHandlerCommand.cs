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
    public class RoleHandlerCommand : ResponseHandler, IRequestHandler<AddRoleCommand, Response<string>>,
                                                       IRequestHandler<EditRoleCommand, Response<string>>,
                                                       IRequestHandler<DeleteRoleCommand, Response<string>>,
                                                       IRequestHandler<UpdateUserRolesCommand, Response<string>>


    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly RoleManager<Role> _roleManager;
        private readonly IAuthorizationService _authorizationService;
        public RoleHandlerCommand(IStringLocalizer<SharedResources> stringLocalizer,
            RoleManager<Role> roleManager, IAuthorizationService authorizationService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _roleManager = roleManager;
            _authorizationService = authorizationService;
        }

        public async Task<Response<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.AddRoleAsync(request.RoleName);
            if (result == "Success")
                return Success("");
            return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.AddFailed]);

        }

        public async Task<Response<string>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.EditRoleAsync(request);
            if (result == "NotFound")
                return NotFound<string>($"Role: {_stringLocalizer[SharedResourcesKeys.NotFound]}");
            else if (result == "Success")
                return Success<string>("");
            else
                return BadRequest<string>(result);
        }

        public async Task<Response<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.DeleteRoleAsync(request.Id);
            if (result == "NotFound")
                return NotFound<string>($"Role: {_stringLocalizer[SharedResourcesKeys.NotFound]}");
            else if (result == "Used")
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.RoleIsUsed]);
            else if (result == "Success")
                return Success<string>(_stringLocalizer[SharedResourcesKeys.Deleted]);
            else
                return BadRequest<string>(result);
        }

        public async Task<Response<string>> Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.UpdateUserRoles(request);
            switch (result)
            {
                case "UserIsNull":
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UserNotFound]);
                    break;
                case "FailedToDeleteoldRoles":
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToDeleteOldRoles]);
                    break;
                case "FailedToAddNewRoles":
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToAddNewRoles]);
                    break;
                case "FailedToUpdateUserRoles":
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUpdateUserRoles]);
                    break;
            }
            return Success<string>(_stringLocalizer[SharedResourcesKeys.Success]);
        }
    }
}
