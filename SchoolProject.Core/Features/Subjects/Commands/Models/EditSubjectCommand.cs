using MediatR;
using SchoolProject.Core.Basics_Status;

namespace SchoolProject.Core.Features.Subjects.Commands.Models
{
    public class EditSubjectCommand : IRequest<Response<string>>
    {
        public int SubjectId { get; set; }
        public string? SubjectNameAr { get; set; }
        public string? SubjectNameEn { get; set; }
        public int? Period { get; set; }
    }
}
