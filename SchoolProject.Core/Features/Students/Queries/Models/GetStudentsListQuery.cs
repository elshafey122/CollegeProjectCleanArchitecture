﻿using MediatR;
using SchoolProject.Core.Basics_Status;
using SchoolProject.Core.Features.Students.Queries.Results;
using SchoolProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Students.Queries.Models
{
    public class GetStudentsListQuery:IRequest<Response<List<StudentListResponse>>>
    {

    }
}
