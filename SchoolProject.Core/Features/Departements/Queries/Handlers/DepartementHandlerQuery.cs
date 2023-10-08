using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Basics_Status;
using SchoolProject.Core.BasicsStatus;
using SchoolProject.Core.Features.Departements.Queries.Models;
using SchoolProject.Core.Features.Departements.Queries.ViewModels;
using SchoolProject.Core.Localization;
using SchoolProject.Core.Wrappings;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstractions;
using System.Linq.Expressions;

namespace SchoolProject.Core.Features.Departements.Queries.Handlers
{
    public class DepartementHandlerQuery : ResponseHandler, IRequestHandler<GetDepardementByIdQuery, Response<DepartementByIdResponse>>
    {
        private readonly IDeparetementService _departementservice;
        private readonly IStudentService _studentservice;

        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        public DepartementHandlerQuery(IDeparetementService departementservice, IMapper mapper,
            IStringLocalizer<SharedResources> stringLocalizer, IStudentService studentservice) : base(stringLocalizer)
        {
            _departementservice = departementservice;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
            _studentservice = studentservice;
        }

        public async Task<Response<DepartementByIdResponse>> Handle(GetDepardementByIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _departementservice.GetDepartementById(request.Id);
            if (response == null)
            {
                return NotFound<DepartementByIdResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            }
            var departementmapper = _mapper.Map<DepartementByIdResponse>(response);
            Expression<Func<Student, StudentResponse>> expression = e => new StudentResponse(e.StuId, e.Localize(e.StuNameAr, e.StuNameEn));
            var studentslist = _studentservice.GetStudentListByDepartementId(request.Id);
            var paginatedlist = await studentslist.Select(expression).ToPaginateListAsync(request.PageNumber, request.PageSize);
            departementmapper.StudentsList = paginatedlist;
            return Success(departementmapper);
        }
    }
}
