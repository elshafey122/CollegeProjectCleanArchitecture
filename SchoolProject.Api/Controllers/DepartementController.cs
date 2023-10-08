using Microsoft.AspNetCore.Mvc;
using SchoolProject.Core.Features.Departements.Queries.Models;
using SchoolProject.Data.ApiRoutingData;

namespace SchoolProject.Api.Controllers
{
    [ApiController]
    public class DepartementController : AppControllerBase
    {

        [HttpGet(Routes.DepartementRouting.GetById)]
        public async Task<IActionResult> GetDepartementById([FromQuery] GetDepardementByIdQuery query)
        {
            var response = await _mediator.Send(query);
            return NewResult(response);
        }
    }
}
