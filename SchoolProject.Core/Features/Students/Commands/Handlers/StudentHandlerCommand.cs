using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Basics_Status;
using SchoolProject.Core.BasicsStatus;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Localization;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstractions;

namespace SchoolProject.Core.Features.Students.Commands.Handlers
{
    public class StudentHandlerCommand : ResponseHandler, IRequestHandler<AddStudentCommand, Response<string>>,
                                                          IRequestHandler<EditStudentCommand, Response<string>>,
                                                          IRequestHandler<DeleteStudentCommand, Response<string>>

    {
        private readonly IStudentService _studentservice;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        public StudentHandlerCommand(IStudentService studentservice, IMapper mapper,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _studentservice = studentservice;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            var studentmapper = _mapper.Map<Student>(request);
            var result = await _studentservice.AddStudent(studentmapper);
            if (result == "Exist")
            {
                return UnprocessableEntity<string>($"Name:{_stringLocalizer[SharedResourcesKeys.IsExist]}");
            }
            else if (result == "Success")
            {
                return Created<string>(_stringLocalizer[SharedResourcesKeys.Created]);
            }
            else
            {
                return BadRequest<string>();
            }
        }

        public async Task<Response<string>> Handle(EditStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentservice.GetStudentByIdWithIncludeAsync(request.Id);
            if (student == null)
            {
                return NotFound<string>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            }
            var studentmapper = _mapper.Map<Student>(request);
            var result = await _studentservice.EditStudent(studentmapper);
            if (result != "Success")
            {
                return BadRequest<string>();
            }
            return Created<string>(_stringLocalizer[SharedResourcesKeys.Created]);
        }

        public async Task<Response<string>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentservice.GetById(request.Id);
            if (student == null)
            {
                return NotFound<string>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            }
            var result = await _studentservice.DeleteStudent(student);
            if (result == "Failure")
            {
                return BadRequest<string>();
            }
            return Deleteed<string>(_stringLocalizer[SharedResourcesKeys.Deleted]);
        }
    }
}
