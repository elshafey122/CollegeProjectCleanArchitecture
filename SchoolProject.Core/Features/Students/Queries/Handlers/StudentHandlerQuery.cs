using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Basics_Status;
using SchoolProject.Core.BasicsStatus;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Core.Features.Students.Queries.Results;
using SchoolProject.Core.Localization;
using SchoolProject.Core.Wrappings;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstractions;
using System.Linq.Expressions;

namespace SchoolProject.Core.Features.Students.Queries.Handlers
{
    public class StudentHandlerQuery : ResponseHandler, IRequestHandler<GetStudentsListQuery, Response<List<StudentListResponse>>>,
                                                        IRequestHandler<GetStudentByIdQuery, Response<StudentbyIdresponse>>,
                                                        IRequestHandler<GetStudentPaginatedListQuery, PaginatedResult<StudentListPaginatedResponse>>

    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IStudentService _studentservice;
        private readonly IMapper _mapper;

        public StudentHandlerQuery(IStudentService studentservice, IMapper mapper,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _studentservice = studentservice;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response<List<StudentListResponse>>> Handle(GetStudentsListQuery request, CancellationToken cancellationToken)
        {
            var studentslist = await _studentservice.GetStudentsListAsync();
            var studentsmapper = _mapper.Map<List<StudentListResponse>>(studentslist);
            var result = Success(studentsmapper);
            result.Meta = new { count = studentsmapper.Count };
            return result;
        }

        public async Task<Response<StudentbyIdresponse>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await _studentservice.GetStudentByIdWithIncludeAsync(request.Id);
            if (student == null)
                return NotFound<StudentbyIdresponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            var studentmapper = _mapper.Map<StudentbyIdresponse>(student);
            return Success(studentmapper);
        }

        public async Task<PaginatedResult<StudentListPaginatedResponse>> Handle(GetStudentPaginatedListQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Student, StudentListPaginatedResponse>> expression = e => new StudentListPaginatedResponse(e.StuId, e.StuNameEn, e.Address, e.Phone, e.Departement.DNameEn);
            var querables = _studentservice.FilterStudentPaginationQuerable(request.OrderBy, request.Search);
            var paginateslist = await querables.Select(expression).ToPaginateListAsync(request.PageNumber, request.PageSize);
            paginateslist.Meta = new { count = paginateslist.Data.Count };
            return paginateslist;
        }
    }
}
