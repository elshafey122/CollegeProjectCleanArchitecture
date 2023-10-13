using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Basics_Status;
using SchoolProject.Core.BasicsStatus;
using SchoolProject.Core.Features.Instructors.Queries.Models;
using SchoolProject.Core.Features.Instructors.Queries.ViewModels;
using SchoolProject.Core.Localization;
using SchoolProject.Core.Wrappings;
using SchoolProject.Service.Abstractions;

namespace SchoolProject.Core.Features.Instructors.Queries.Handlers
{
    public class InstructorHansdlerQuery : ResponseHandler, IRequestHandler<GetInstructorListQuery, Response<PaginatedResult<GetInstructorResponse>>>,
                                                            IRequestHandler<GetInstructorByIdQuery, Response<GetInstructorResponse>>
    {
        private readonly IInstructorService _instructorService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        public InstructorHansdlerQuery(IInstructorService instructorService, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _instructorService = instructorService;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response<PaginatedResult<GetInstructorResponse>>> Handle(GetInstructorListQuery request, CancellationToken cancellationToken)
        {
            var response = _instructorService.GetInstructorList();
            var paginatedinstructor = await response.Select(e =>
                new GetInstructorResponse
                {
                    InstructorName = e.Localize(e.NameAr, e.NameEn),
                    InstructorId = e.InsId,
                    Address = e.Address,
                    Position = e.Position,
                    Salary = e.Salary,
                    DepartementName = e.Departement.Localize(e.Departement.DNameAr, e.Departement.DNameEn),
                    SupervisorName = e.supervisor.NameEn,
                })
                .ToPaginateListAsync(request.PageNumber, request.PageSize);

            return Success(paginatedinstructor);
        }

        public async Task<Response<GetInstructorResponse>> Handle(GetInstructorByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _instructorService.GetInstructorbyId(request.Id);
            if (result == null)
            {
                return NotFound<GetInstructorResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            }
            var instructormapper = _mapper.Map<GetInstructorResponse>(result);
            return Success(instructormapper);
        }
    }
}
