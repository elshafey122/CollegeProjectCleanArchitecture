using Microsoft.AspNetCore.Mvc;
using SchoolProject.Core.Features.Authentication.Commands.Models;
using SchoolProject.Core.Features.Authentication.Queires.Modles;
using SchoolProject.Data.ApiRoutingData;

namespace SchoolProject.Api.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticationController : AppControllerBase
    {

        [HttpPost(Routes.Authentication.SignIn)]
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
    }
}
