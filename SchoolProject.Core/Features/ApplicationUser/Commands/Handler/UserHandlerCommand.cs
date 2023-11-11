using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Basics_Status;
using SchoolProject.Core.BasicsStatus;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Core.Localization;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.Handler
{
    public class UserHandlerCommand : ResponseHandler, IRequestHandler<AddUserCommand, Response<string>>,
                                                       IRequestHandler<EditUserCommand, Response<string>>,
                                                       IRequestHandler<DeleteUserCommand, Response<string>>,
                                                       IRequestHandler<ChangeUserPasswordCommand, Response<string>>


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
            if (Userbyusername != null)
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.NameIsExist]);

            // mapping to current user
            var IdentityUser = _mapper.Map<User>(request);

            // create new user
            var createdresult = await _userManager.CreateAsync(IdentityUser, request.Password);//
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

            var Userbyusername = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == request.UserName && x.Id != user.Id);
            if (Userbyusername != null)
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.NameIsExist]);

            var newuser = _mapper.Map(request, user);

            var result = await _userManager.UpdateAsync(newuser);
            if (!result.Succeeded)
            {
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UpdateFailed]);
            }
            return Success<string>(_stringLocalizer[SharedResourcesKeys.Updated]);
        }

        public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null)
                return NotFound<string>(_stringLocalizer[SharedResourcesKeys.NotFound]);

            var result = await _userManager.DeleteAsync(user);//
            if (!result.Succeeded)
            {
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.DeletedFailed]);
            }
            return Success<string>(_stringLocalizer[SharedResourcesKeys.Deleted]);
        }

        public async Task<Response<string>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null)
                return NotFound<string>(_stringLocalizer[SharedResourcesKeys.NotFound]);

            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);//
            if (!result.Succeeded)
                return BadRequest<string>(result.Errors.FirstOrDefault().Description);
            return Success<string>(_stringLocalizer[SharedResourcesKeys.Success]);

        }
    }
}
