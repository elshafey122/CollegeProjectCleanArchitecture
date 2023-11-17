using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Basics_Status;
using SchoolProject.Core.BasicsStatus;
using SchoolProject.Core.Features.Authorization.Queires.Model;
using SchoolProject.Core.Features.Authorization.Queires.ViewModel;
using SchoolProject.Core.Localization;
using SchoolProject.Service.Abstractions;

namespace SchoolProject.Core.Features.Authorization.Queires.Handler
{
    public class RolesHandlerQueires : ResponseHandler, IRequestHandler<GetRolesListQuery, Response<List<GetRolesViewMode>>>,
                                                        IRequestHandler<GetRoleByIdQuery, Response<GetRoleByIdViewMode>>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IAuthorizationService _authorizationService;
        private readonly IMapper _mapper;
        public RolesHandlerQueires(IStringLocalizer<SharedResources> stringLocalizer,
            IAuthorizationService authorizationService, IMapper mapper) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _authorizationService = authorizationService;
            _mapper = mapper;
        }

        public async Task<Response<List<GetRolesViewMode>>> Handle(GetRolesListQuery request, CancellationToken cancellationToken)
        {
            var roles = await _authorizationService.GetRolesList();
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
    }
}
