using Microsoft.AspNetCore.Mvc;
using SchoolProject.Core.Features.Authentication.Commands.Models;
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
    }
}
