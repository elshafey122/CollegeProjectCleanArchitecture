using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Basics_Status;
using SchoolProject.Core.BasicsStatus;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Core.Localization;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.Handler
{
    public class UserHandlerCommand : ResponseHandler, IRequestHandler<AddUserCommand, Response<string>>,
                                                       IRequestHandler<EditUserCommand, Response<string>>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly UserManager<User> _userManager;

        public UserHandlerCommand(IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer, UserManager<User> userManager) : base(stringLocalizer)
        {
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
            _userManager = userManager;
        }

        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            //check exist email
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user != null)
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.EmailIsExist]);

            //check exist username
            var Userbyusername = await _userManager.FindByNameAsync(request.UserName);
            if (user != null)
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.NameIsExist]);

            // mapping to current user
            var IdentityUser = _mapper.Map<User>(request);

            // create new user
            var createdresult = await _userManager.CreateAsync(IdentityUser, request.Password);
            if (!createdresult.Succeeded)
            {
                return BadRequest<string>(createdresult.Errors.FirstOrDefault().Description);
            }

            return Success("");
        }

        public async Task<Response<string>> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null)
                return NotFound<string>(_stringLocalizer[SharedResourcesKeys.NotFound]);

            var newuser = _mapper.Map(request, user);

            var result = await _userManager.UpdateAsync(newuser);
            if (!result.Succeeded)
            {
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UpdateFailed]);
            }
            return Success<string>(_stringLocalizer[SharedResourcesKeys.Updated]);
        }

    }
}
