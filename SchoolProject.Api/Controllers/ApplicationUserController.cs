using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Data.ApiRoutingData;
using System.Net;

namespace SchoolProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : AppControllerBase

    {
        private readonly IValidator<AddUserCommand> _adduservalidator;
        public ApplicationUserController(IValidator<AddUserCommand> adduservalidator)
        {
            _adduservalidator = adduservalidator;
        }

        [HttpPost(Routes.ApplicationUserRouting.Create)]
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
                    Success = false,
                };
                return BadRequest(errorresponse);
            }
            var response = await _mediator.Send(command);
            return NewResult(response);

        }
    }
}
