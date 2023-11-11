using FluentValidation;
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
    public class ApplicationUserController : AppControllerBase

    {
        private readonly IValidator<AddUserCommand> _adduservalidator;
        private readonly IValidator<EditUserCommand> _edituservalidator;
        private readonly IValidator<ChangeUserPasswordCommand> _changeuserpasswordvalidator;

        public ApplicationUserController(IValidator<AddUserCommand> adduservalidator, IValidator<EditUserCommand> edituservalidator,
             IValidator<ChangeUserPasswordCommand> changeuserpasswordvalidator)
        {
            _adduservalidator = adduservalidator;
            _edituservalidator = edituservalidator;
            _changeuserpasswordvalidator = changeuserpasswordvalidator;
        }

        [HttpPost(Routes.ApplicationUserRouting.Register)]
        public async Task<IActionResult> CreateUser([FromBody] AddUserCommand command)
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
            return Ok(response);
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
            return Ok(response);
        }

        [HttpDelete(Routes.ApplicationUserRouting.Delete)]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            var response = await _mediator.Send(new DeleteUserCommand { Id = id });
            return Ok(response);
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
            return Ok(response);
        }
    }
}
