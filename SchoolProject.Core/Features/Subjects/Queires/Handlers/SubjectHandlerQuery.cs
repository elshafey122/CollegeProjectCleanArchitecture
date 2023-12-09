using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Basics_Status;
using SchoolProject.Core.BasicsStatus;
using SchoolProject.Core.Features.Subjects.Queires.Models;
using SchoolProject.Core.Features.Subjects.Queires.ViewModels;
using SchoolProject.Core.Localization;
using SchoolProject.Core.Wrappings;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstractions;
using System.Linq.Expressions;

namespace SchoolProject.Core.Features.Subjects.Queires.Handlers
{
    public class SubjectHandlerQuery : ResponseHandler, IRequestHandler<GetSubjectsPaginatedListQuery, PaginatedResult<SubjectsPaginatedListResponse>>,
                                                        IRequestHandler<GetSubjectById, Response<SubjectByIdResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly ISubjectService _subjectService;
        public SubjectHandlerQuery(IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer, ISubjectService subjectService) : base(stringLocalizer)
        {
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
            _subjectService = subjectService;
        }

        public async Task<PaginatedResult<SubjectsPaginatedListResponse>> Handle(GetSubjectsPaginatedListQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Subject, SubjectsPaginatedListResponse>> expression = e => new SubjectsPaginatedListResponse { Period = e.Period, SubID = e.SubID, SubjectName = e.Localize(e.SubNameAr, e.SubNameEn) };
            var subjectsFiltering = _subjectService.FilterSubjectsPaginationQuerable(request.OrderBy, request.Search);
            var subjectsPaginated = await subjectsFiltering.Select(expression).ToPaginateListAsync(request.PageNumber, request.PageSize);
            subjectsPaginated.Meta = new { count = subjectsPaginated.Data.Count };
            return subjectsPaginated;
        }

        public async Task<Response<SubjectByIdResponse>> Handle(GetSubjectById request, CancellationToken cancellationToken)
        {
            var subject = await _subjectService.GetSubjectById(request.SubjectId);
            if (subject == null)
                return NotFound<SubjectByIdResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            var subjectMapper = _mapper.Map<SubjectByIdResponse>(subject);
            return Success<SubjectByIdResponse>(subjectMapper);
        }
    }
}