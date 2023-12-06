using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Core.Features.Instructors.Commands.Models;
using SchoolProject.Core.Features.Instructors.Queries.Models;
using SchoolProject.Data.ApiRoutingData;
using System.Net;

namespace SchoolProject.Api.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class InstructorController : AppControllerBase
    {
        private readonly IValidator<AddInstructorCommand> _addinstructorvalidator;
        private readonly IValidator<EditInstructorCommand> _editinstructorvalidator;
        public InstructorController(IValidator<AddInstructorCommand> addinstructorvalidator, IValidator<EditInstructorCommand> editinstructorvalidator)
        {
            _addinstructorvalidator = addinstructorvalidator;
            _editinstructorvalidator = editinstructorvalidator;
        }

        [HttpGet(Routes.InstructorRouting.List)]
        public async Task<IActionResult> GetInstructorPaginated([FromQuery] GetInstructorListQuery query)
        {
            var response = await _mediator.Send(query);
            return NewResult(response);
        }

        [HttpGet(Routes.InstructorRouting.GetById)]
        public async Task<IActionResult> GetInstructorById([FromRoute] int id)
        {
            var response = await _mediator.Send(new GetInstructorByIdQuery { Id = id });
            return NewResult(response);
        }

        [HttpPost(Routes.InstructorRouting.Create)]
        public async Task<IActionResult> AddInstructor([FromBody] AddInstructorCommand query)
        {
            var validation = await _addinstructorvalidator.ValidateAsync(query);
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

        [HttpPut(Routes.InstructorRouting.Edit)]
        public async Task<IActionResult> EditInstructor([FromBody] EditInstructorCommand query)
        {
            var validation = await _editinstructorvalidator.ValidateAsync(query);
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

        [HttpDelete(Routes.InstructorRouting.Delete)]
        public async Task<IActionResult> DeleteInstructor([FromRoute] int id)
        {
            var response = await _mediator.Send(new DeleteInstructorCommand { Id = id });
            return NewResult(response);
        }

        [HttpGet(Routes.InstructorRouting.GetSummationSalaryOfInstructors)]
        public async Task<IActionResult> GetSummationSalaryOfInstructors()
        {
            var response = await _mediator.Send(new GetSummationSalaryOfInstructorsQuery());
            return NewResult(response);
        }

        [HttpGet(Routes.InstructorRouting.GetInstructorsSalaryData)]
        public async Task<IActionResult> GetInstructorsSalaryData()
        {
            var response = await _mediator.Send(new GetInstructorsSalaryDataQuery());
            return NewResult(response);
        }
    }
}
