using MediatR;
using SchoolProject.Core.Basics_Status;

namespace SchoolProject.Core.Features.Subjects.Commands.Models
{
    public class AddSubjectCommand : IRequest<Response<string>>
    {
        public string? SubjectNameAr { get; set; }
        public string? SubjectNameEn { get; set; }
        public int? Period { get; set; }
    }
}
