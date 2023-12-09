using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Core.Features.ApplicationUser.Queires.Model;
using SchoolProject.Core.Features.ApplicationUser.Queires.NewFolder.Model;
using SchoolProject.Data.ApiRoutingData;
using System.Net;

namespace SchoolProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,User")]
    public class ApplicationUserController : AppControllerBase

    {
        private readonly IValidator<RegisterUserCommand> _adduservalidator;
        private readonly IValidator<EditUserCommand> _edituservalidator;
        private readonly IValidator<ChangeUserPasswordCommand> _changeuserpasswordvalidator;

        public ApplicationUserController(IValidator<RegisterUserCommand> adduservalidator, IValidator<EditUserCommand> edituservalidator,
             IValidator<ChangeUserPasswordCommand> changeuserpasswordvalidator)
        {
            _adduservalidator = adduservalidator;
            _edituservalidator = edituservalidator;
            _changeuserpasswordvalidator = changeuserpasswordvalidator;
        }
        [AllowAnonymous] // that tell that you not has authorize and any one can access this point 
        [HttpPost(Routes.ApplicationUserRouting.Register)]
        public async Task<IActionResult> CreateUser([FromBody] RegisterUserCommand command)
        {
            var validation = await _adduservalidator.ValidateAsync(command);
            if (!validation.IsValid)
            {
                var errorMessages = validation.Errors.Select(error => error.ErrorMessage);
                var errorresponse = new ErrorValidationResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    ErrorMessages = errorMessages,
                };
                return BadRequest(errorresponse);
            }
            var response = await _mediator.Send(command);
            return NewResult(response);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet(Routes.ApplicationUserRouting.GetUsers)]
        public async Task<IActionResult> GetAllUsers([FromQuery] GetUsersPaginatedQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet(Routes.ApplicationUserRouting.GetbyId)]
        public async Task<IActionResult> GetuserById([FromRoute] int id)
        {
            var response = await _mediator.Send(new GetUserByIdQuery(id));
            return NewResult(response);
        }

        [HttpPut(Routes.ApplicationUserRouting.Edit)]
        public async Task<IActionResult> UpdateUser([FromBody] EditUserCommand command)
        {
            var validation = await _edituservalidator.ValidateAsync(command);
            if (!validation.IsValid)
            {
                var errorMessages = validation.Errors.Select(error => error.ErrorMessage);
                var errorresponse = new ErrorValidationResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    ErrorMessages = errorMessages,
                };
                return BadRequest(errorresponse);
            }
            var response = await _mediator.Send(command);
            return NewResult(response);
        }

        [HttpDelete(Routes.ApplicationUserRouting.Delete)]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            var response = await _mediator.Send(new DeleteUserCommand { Id = id });
            return NewResult(response);
        }

        [HttpPut(Routes.ApplicationUserRouting.ChangeUserPassword)]
        public async Task<IActionResult> ChangeUserPassword([FromBody] ChangeUserPasswordCommand command)
        {
            var validation = await _changeuserpasswordvalidator.ValidateAsync(command);
            if (!validation.IsValid)
            {
                var errorMessages = validation.Errors.Select(error => error.ErrorMessage);
                var errorresponse = new ErrorValidationResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    ErrorMessages = errorMessages,
                };
                return BadRequest(errorresponse);
            }
            var response = await _mediator.Send(command);
            return NewResult(response);
        }
    }
}
