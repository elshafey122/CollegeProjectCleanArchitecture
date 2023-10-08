using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Students.Queries.Results
{
    public class StudentListResponse
    {
        public int? StuId { get; set; }
        public string? StuName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? DepartementName { get; set; }

    }
}
