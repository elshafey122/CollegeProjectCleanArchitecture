

using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Basics_Status;
using SchoolProject.Core.BasicsStatus;
using SchoolProject.Core.Features.Authorization.Queires.Model;
using SchoolProject.Core.Localization;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Results;
using SchoolProject.Service.Abstractions;

namespace SchoolProject.Core.Features.Authorization.Queires.Handler
{
    public class ClaimsQueryHandler : ResponseHandler,
                                       IRequestHandler<ManageUserClaimsQuery, Response<ManageUserClaimsResult>>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IAuthorizationService _authorizationService;
        public ClaimsQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,
            IAuthorizationService authorizationService, IMapper mapper, UserManager<User> userManager) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _authorizationService = authorizationService;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<Response<ManageUserClaimsResult>> Handle(ManageUserClaimsQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
                return NotFound<ManageUserClaimsResult>(_stringLocalizer[SharedResourcesKeys.UserNotFound]);

            var result = await _authorizationService.ManageuserClaimsData(user);
            return Success(result);
        }
    }
}
