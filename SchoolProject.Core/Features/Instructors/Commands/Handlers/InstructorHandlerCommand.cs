using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Basics_Status;
using SchoolProject.Core.BasicsStatus;
using SchoolProject.Core.Features.Instructors.Commands.Models;
using SchoolProject.Core.Localization;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstractions;

namespace SchoolProject.Core.Features.Instructors.Commands.Handlers
{
    public class InstructorHandlerCommand : ResponseHandler, IRequestHandler<AddInstructorCommand, Response<string>>,
                                                             IRequestHandler<EditInstructorCommand, Response<string>>,
                                                             IRequestHandler<DeleteInstructorCommand, Response<string>>
    {
        private readonly IInstructorService _instructorService;
        private readonly IWebHostEnvironment _webEnvironment;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        public InstructorHandlerCommand(IInstructorService instructorService, IMapper mapper,
            IStringLocalizer<SharedResources> stringLocalizer, IWebHostEnvironment webEnvironment) : base(stringLocalizer)
        {
            _instructorService = instructorService;
            _webEnvironment = webEnvironment;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }
        public async Task<Response<string>> Handle(AddInstructorCommand request, CancellationToken cancellationToken)
        {
            var locationWwwrootImage = _webEnvironment.WebRootPath + "/Instructors/";  // path for folder wwwroot in api project 
            var instructormapper = _mapper.Map<Instructor>(request);
            var result = await _instructorService.AddInstructor(instructormapper, request.Image, locationWwwrootImage);
            switch (result)
            {
                case "NoImage":
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.NoImage]);
                case "FailedToUploadImage":
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUploadImage]);
                case "FailedInAdd":
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.AddFailed]);
            }
            return Success<string>(_stringLocalizer[SharedResourcesKeys.Success]);
        }

        public async Task<Response<string>> Handle(EditInstructorCommand request, CancellationToken cancellationToken)
        {
            var locationWwwrootImage = _webEnvironment.WebRootPath + "/Instructors/";  // path for folder wwwroot in api project 
            var instructormapper = _mapper.Map<Instructor>(request);
            var result = await _instructorService.EditInstructor(instructormapper, request.Image, locationWwwrootImage);
            switch (result)
            {
                case "NoImage":
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.NoImage]);
                case "FailedToUploadImage":
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUploadImage]);
                case "FailedInAdd":
                    return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.AddFailed]);
            }
            return Success<string>(_stringLocalizer[SharedResourcesKeys.Success]);
        }

        public async Task<Response<string>> Handle(DeleteInstructorCommand request, CancellationToken cancellationToken)
        {
            var result = await _instructorService.DeleteInstructor(request.Id);
            if (result == "NotFound")
            {
                return NotFound<string>($"instructor: {_stringLocalizer[SharedResourcesKeys.NotFound]}");
            }
            else if (result == "Success")
            {
                return Success<string>(_stringLocalizer[SharedResourcesKeys.Success]);
            }
            else
            {
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.BadRequest]);
            }
        }
    }
}
