using MediatR;
using SchoolProject.Core.Basics_Status;
using SchoolProject.Core.Features.Students.Queries.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Students.Queries.Models
{
    public class GetStudentByIdQuery:IRequest<Response<StudentbyIdresponse>>
    {
        public int Id { get; set; }
        public GetStudentByIdQuery(int id)
        {
            Id = id;
        }
    }
}
