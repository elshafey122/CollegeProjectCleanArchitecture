using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Core.Features.Subjects.Commands.Models;
using SchoolProject.Core.Features.Subjects.Queires.Models;
using SchoolProject.Data.ApiRoutingData;
using System.Net;

namespace SchoolProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,User")]
    public class SubjectController : AppControllerBase
    {
        private readonly IValidator<AddSubjectCommand> _addsubjectvalidator;
        private readonly IValidator<EditSubjectCommand> _editsubjectvalidator;

        public SubjectController(IValidator<AddSubjectCommand> addsubjectvalidator, IValidator<EditSubjectCommand> editsubjectvalidator)
        {
            _addsubjectvalidator = addsubjectvalidator;
            _editsubjectvalidator = editsubjectvalidator;
        }

        [HttpGet(Routes.SubjectRouting.List)]
        public async Task<IActionResult> GetPaginatedSubjects([FromQuery] GetSubjectsPaginatedListQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet(Routes.SubjectRouting.GetById)]
        public async Task<IActionResult> GetSubjectById([FromRoute] int id)
        {
            var result = await _mediator.Send(new GetSubjectById() { SubjectId = id });
            return NewResult(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost(Routes.SubjectRouting.Create)]
        public async Task<IActionResult> AddSubject([FromBody] AddSubjectCommand command)
        {
            var validation = await _addsubjectvalidator.ValidateAsync(command);
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

            var result = await _mediator.Send(command);
            return NewResult(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut(Routes.SubjectRouting.Edit)]
        public async Task<IActionResult> EditSubject([FromBody] EditSubjectCommand command)
        {
            var validation = await _editsubjectvalidator.ValidateAsync(command);
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

            var result = await _mediator.Send(command);
            return NewResult(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete(Routes.SubjectRouting.GetById)]
        public async Task<IActionResult> DeleteSubject([FromRoute] int id)
        {
            var result = await _mediator.Send(new DeleteSubjectCommand() { SubjectId = id });
            return Ok(result);
        }
    }
}
