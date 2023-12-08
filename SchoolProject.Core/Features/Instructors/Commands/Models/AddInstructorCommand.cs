using MediatR;
using Microsoft.AspNetCore.Http;
using SchoolProject.Core.Basics_Status;

namespace SchoolProject.Core.Features.Instructors.Commands.Models
{
    public class AddInstructorCommand : IRequest<Response<string>>
    {
        public string? InstructotNameAr { get; set; }
        public string? InstructorNameEn { get; set; }
        public string? Position { get; set; }
        public string? Address { get; set; }
        public decimal? Salary { get; set; }
        public int? SupervisorId { get; set; }
        public int? DepartementId { get; set; }
        public IFormFile? Image { get; set; }

    }
}
