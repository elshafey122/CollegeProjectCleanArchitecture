using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Basics_Status;
using SchoolProject.Core.BasicsStatus;
using SchoolProject.Core.Features.Subjects.Commands.Models;
using SchoolProject.Core.Localization;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstractions;

namespace SchoolProject.Core.Features.Subjects.Commands.Commands
{
    public class SubjectHandlerCommand : ResponseHandler, IRequestHandler<AddSubjectCommand, Response<string>>,
                                                          IRequestHandler<EditSubjectCommand, Response<string>>,
                                                          IRequestHandler<DeleteSubjectCommand, Response<string>>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly ISubjectService _subjectService;
        public SubjectHandlerCommand(IStringLocalizer<SharedResources> stringLocalizer, IMapper mapper, ISubjectService subjectService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _subjectService = subjectService;
        }
        public async Task<Response<string>> Handle(AddSubjectCommand request, CancellationToken cancellationToken)
        {
            var Subjectmapper = _mapper.Map<Subject>(request);
            var result = await _subjectService.AddSubjectAsync(Subjectmapper);
            if (result == "Success")
                return Success<string>(_stringLocalizer[SharedResourcesKeys.Success]);
            return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.AddFailed]);
        }

        public async Task<Response<string>> Handle(EditSubjectCommand request, CancellationToken cancellationToken)
        {
            var Subjectmapper = _mapper.Map<Subject>(request);
            var result = await _subjectService.EditSubjectAsync(Subjectmapper);
            if (result == "NotFound")
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            else if (result == "FailedToUpdate")
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.AddFailed]);

            return Success<string>(_stringLocalizer[SharedResourcesKeys.Success]);
        }

        public async Task<Response<string>> Handle(DeleteSubjectCommand request, CancellationToken cancellationToken)
        {
            var result = await _subjectService.DeleteSubjectAsync(request.SubjectId);
            if (result == "NotFound")
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            else if (result == "FailedToDelete")
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.DeletedFailed]);

            return Success<string>(_stringLocalizer[SharedResourcesKeys.Success]);
        }
    }
}
