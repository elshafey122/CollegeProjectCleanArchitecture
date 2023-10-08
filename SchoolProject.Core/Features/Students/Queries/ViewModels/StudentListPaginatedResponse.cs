namespace SchoolProject.Core.Features.Students.Queries.Results
{
    public class StudentListPaginatedResponse
    {
        public int? StuId { get; set; }
        public string? StuName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? DepartementName { get; set; }
        public StudentListPaginatedResponse(int? stuId, string? stuName, string? address, string? phone, string? departementName)
        {
            StuId = stuId;
            StuName = stuName;
            Address = address;
            Phone = phone;
            DepartementName = departementName;
        }
    }
}
