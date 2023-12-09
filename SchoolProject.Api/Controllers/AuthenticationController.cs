using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Core.Features.Authentication.Commands.Models;
using SchoolProject.Core.Features.Authentication.Queires.Modles;
using SchoolProject.Data.ApiRoutingData;

namespace SchoolProject.Api.Controllers
{
    public class AuthenticationController : AppControllerBase
    {
        [HttpPost(Routes.Authentication.SignIn)]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn([FromForm] SignInCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost(Routes.Authentication.GenerateRefreshToken)]
        public async Task<IActionResult> GenerateRefreshToken([FromForm] RefreshTokenCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet(Routes.Authentication.ValidateToken)]
        public async Task<IActionResult> ValidateToken([FromQuery] AuthorizeUserTokenQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet(Routes.Authentication.ConfirmEmail)]
        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpPost(Routes.Authentication.SendResetPassword)]
        public async Task<IActionResult> SendResetPassword([FromQuery] SendResetPasswordCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet(Routes.Authentication.ConfirmResetpassword)]
        public async Task<IActionResult> ConfirmResetpassword([FromQuery] ConfirmResetPasswordQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpPost(Routes.Authentication.ResetPassword)]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

    }
}
