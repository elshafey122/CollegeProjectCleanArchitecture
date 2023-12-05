using Microsoft.AspNetCore.Mvc;
using SchoolProject.Core.Features.Email.Commands.Models;
using SchoolProject.Data.ApiRoutingData;

namespace SchoolProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : AppControllerBase
    {
        public EmailController()
        {

        }
        [HttpGet(Routes.Email.SendEmail)]
        public async Task<IActionResult> SendEmail([FromQuery] SendEmaiCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }

    }
}
