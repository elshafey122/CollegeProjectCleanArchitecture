using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Basics_Status;
using SchoolProject.Core.BasicsStatus;
using SchoolProject.Core.Features.Authorization.Queires.Model;
using SchoolProject.Core.Features.Authorization.Queires.ViewModel;
using SchoolProject.Core.Localization;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Results;
using SchoolProject.Service.Abstractions;

namespace SchoolProject.Core.Features.Authorization.Queires.Handler
{
    public class RolesQueiresHandler : ResponseHandler,
                                       IRequestHandler<GetRolesListQuery, Response<List<GetRolesViewMode>>>,
                                       IRequestHandler<GetRoleByIdQuery, Response<GetRoleByIdViewMode>>,
                                       IRequestHandler<ManageUserRolesQuery, Response<ManageUserRolesResult>> // manage = get roles of a user and indicate its real roles
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IAuthorizationService _authorizationService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public RolesQueiresHandler(IStringLocalizer<SharedResources> stringLocalizer,
            IAuthorizationService authorizationService, IMapper mapper, UserManager<User> userManager) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _authorizationService = authorizationService;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<Response<List<GetRolesViewMode>>> Handle(GetRolesListQuery request, CancellationToken cancellationToken)
        {
            var roles = await _authorizationService.MnageUserRoles();
            var mapperRoles = _mapper.Map<List<GetRolesViewMode>>(roles);
            return Success(mapperRoles);
        }

        public async Task<Response<GetRoleByIdViewMode>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _authorizationService.GetRoleById(request.Id);
            if (role == null)
                return NotFound<GetRoleByIdViewMode>($"role: {_stringLocalizer[SharedResourcesKeys.RoleIsNotExist]}");
            var ruleMapper = _mapper.Map<GetRoleByIdViewMode>(role);
            return Success(ruleMapper);
        }

        public async Task<Response<ManageUserRolesResult>> Handle(ManageUserRolesQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
                return NotFound<ManageUserRolesResult>(_stringLocalizer[SharedResourcesKeys.UserNotFound]);

            var result = await _authorizationService.ManageuserRolesData(user);
            return Success(result);
        }
    }
}
