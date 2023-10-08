using SchoolProject.Core.Wrappings;

namespace SchoolProject.Core.Features.Departements.Queries.ViewModels
{
    public class DepartementByIdResponse
    {
        public int? Id { get; set; }
        public string? DepartementName { get; set; }
        public string? InstructorManager { get; set; }
        public PaginatedResult<StudentResponse>? StudentsList { get; set; }
        public List<SubjectResponse>? SubjectsList { get; set; }
        public List<InstuctorResponse>? InstuctorsList { get; set; }
    }

    public class StudentResponse
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public StudentResponse(int? id, string? name)
        {
            Id = id;
            Name = name;
        }
    }
    public class SubjectResponse
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
    }
    public class InstuctorResponse
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
    }
}
