namespace SchoolProject.Core.Features.Instructors.Queries.ViewModels
{
    public class GetInstructorResponse
    {
        public int InstructorId { get; set; }//
        public string? InstructorName { get; set; }//
        public string? Position { get; set; }
        public string? Address { get; set; }
        public decimal? Salary { get; set; }
        public string? SupervisorName { get; set; }//
        public string? DepartementName { get; set; }//
    }
}
