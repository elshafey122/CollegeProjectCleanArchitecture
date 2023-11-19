using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Core.Features.Authorization.Commnands.Models;
using SchoolProject.Core.Features.Authorization.Queires.Model;
using SchoolProject.Data.ApiRoutingData;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace SchoolProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AuthorizationController : AppControllerBase
    {
        private readonly IValidator<AddRoleCommand> _addRoleValidator;
        private readonly IValidator<EditRoleCommand> _editRoleValidator;
        private readonly IValidator<DeleteRoleCommand> _deleteRoleValidator;

        public AuthorizationController(IValidator<AddRoleCommand> addRoleValidator,
            IValidator<EditRoleCommand> editRoleValidator,
            IValidator<DeleteRoleCommand> deleteRoleValidator)
        {
            _addRoleValidator = addRoleValidator;
            _editRoleValidator = editRoleValidator;
            _deleteRoleValidator = deleteRoleValidator;
        }

        [HttpPost(Routes.Authorization.Create)]
        public async Task<IActionResult> CreateRole([FromForm] AddRoleCommand command)
        {
            var validation = await _addRoleValidator.ValidateAsync(command);
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


        [HttpPut(Routes.Authorization.Edit)]
        public async Task<IActionResult> EditRole([FromForm] EditRoleCommand command)
        {
            var validation = await _editRoleValidator.ValidateAsync(command);
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

        [HttpDelete(Routes.Authorization.Delete)]
        public async Task<IActionResult> DeleteRole([FromRoute] int id)
        {
            DeleteRoleCommand command = new DeleteRoleCommand() { Id = id };
            var validation = await _deleteRoleValidator.ValidateAsync(command);
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

            var response = await _mediator.Send(new DeleteRoleCommand { Id = id });
            return NewResult(response);
        }


        [HttpGet(Routes.Authorization.List)]
        public async Task<IActionResult> GetAllRoles()
        {
            var response = await _mediator.Send(new GetRolesListQuery());
            return NewResult(response);
        }

        [HttpGet(Routes.Authorization.GetById)]
        public async Task<IActionResult> GetRoleById(int id)
        {
            var response = await _mediator.Send(new GetRoleByIdQuery() { Id = id });
            return NewResult(response);
        }

        [SwaggerOperation(Summary = "ادارة صلاحية المستخدمين", OperationId = "ManageUserRoles")]

        [HttpGet(Routes.Authorization.ManageUserRoles)]
        public async Task<IActionResult> ManageUserRoles([FromRoute] int id)
        {
            var response = await _mediator.Send(new ManageUserRolesQuery { UserId = id });
            return Ok(response);
        }

        [SwaggerOperation(Summary = "تعديل صلاحية المستخدمين", OperationId = "UpdateUserRoles")]

        [HttpPut(Routes.Authorization.UpdateUserRoles)]
        public async Task<IActionResult> UpdateUserRoles([FromBody] UpdateUserRolesCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        //claims 
        [SwaggerOperation(Summary = "ادارة صلاحية الاستخدام للمستخدمين", OperationId = "ManageUserClaims")]
        [HttpGet(Routes.Authorization.ManageUserClaims)]
        public async Task<IActionResult> ManageUserClaims([FromRoute] int id)
        {
            var response = await _mediator.Send(new ManageUserClaimsQuery { UserId = id });
            return Ok(response);
        }

        [SwaggerOperation(Summary = "تعديل صلاحية الاستخدام للمستخدمين", OperationId = "UpdateUserClaims")]

        [HttpPut(Routes.Authorization.UpdateUserClaims)]
        public async Task<IActionResult> UpdateUserClaims([FromBody] UpdateUserClaimsCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

    }
}