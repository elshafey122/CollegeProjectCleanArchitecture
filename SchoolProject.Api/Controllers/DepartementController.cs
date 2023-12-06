using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Core.Features.Departements.Commands.Models;
using SchoolProject.Core.Features.Departements.Queries.Models;
using SchoolProject.Data.ApiRoutingData;
using System.Net;

namespace SchoolProject.Api.Controllers
{
    [ApiController]

    [Authorize(Roles = "Admin")]
    public class DepartementController : AppControllerBase
    {
        private readonly IValidator<AddDepartementCommand> _adddepvalidator;
        //private readonly IValidator<EditDepartementCommand> _editdepvalidator;
        public DepartementController(IValidator<AddDepartementCommand> Addvalidator)
        {
            _adddepvalidator = Addvalidator;
            //_editdepvalidator = Editvalidator;
        }

        [HttpGet(Routes.DepartementRouting.GetById)]
        public async Task<IActionResult> GetDepartementById([FromQuery] GetDepardementByIdQuery query)
        {
            var response = await _mediator.Send(query);
            return NewResult(response);
        }

        [HttpGet(Routes.DepartementRouting.List)]
        public async Task<IActionResult> GetPaginatedDepartementList([FromQuery] GetDepartementListQuery query)
        {
            var response = await _mediator.Send(query);
            return NewResult(response);
        }
        [AllowAnonymous]
        [HttpPost(Routes.DepartementRouting.Create)]
        public async Task<IActionResult> AddDepartement([FromBody] AddDepartementCommand query)
        {
            var validation = await _adddepvalidator.ValidateAsync(query);
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
            var response = await _mediator.Send(query);
            return NewResult(response);
        }

        [HttpPut(Routes.DepartementRouting.Edit)]
        public async Task<IActionResult> EditDepartement([FromBody] EditDepartementCommand query)
        {
            //var validation = await _editdepvalidator.ValidateAsync(query);
            //if (!validation.IsValid)
            //{
            //    var errorMessages = validation.Errors.Select(error => error.ErrorMessage);
            //    var errorresponse = new ErrorValidationResponse
            //    {
            //        StatusCode = HttpStatusCode.BadRequest,
            //        ErrorMessages = errorMessages,
            //    };
            //    return BadRequest(errorresponse);
            //}
            var response = await _mediator.Send(query);
            return NewResult(response);
        }

        [HttpDelete(Routes.DepartementRouting.Delete)]
        public async Task<IActionResult> DeleteDepartement([FromRoute] int id)
        {
            var response = await _mediator.Send(new DeleteDepartementCommand { DId = id });
            return NewResult(response);
        }

        [HttpGet(Routes.DepartementRouting.GetDepartementStudentCount)]
        public async Task<IActionResult> GetDepartementStudentCount()
        {
            var response = await _mediator.Send(new GetDepartementStudentListCountQurery());
            return NewResult(response);
        }

        [HttpGet(Routes.DepartementRouting.GetDepartstudentContById)]
        public async Task<IActionResult> GetDepartstudentContById([FromRoute] int id)
        {
            var response = await _mediator.Send(new GetDepartementStudentCountByIdQuery { DId = id });
            return NewResult(response);
        }
    }
}
