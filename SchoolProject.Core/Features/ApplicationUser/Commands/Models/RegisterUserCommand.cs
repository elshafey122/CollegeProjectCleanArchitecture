﻿using MediatR;
using SchoolProject.Core.Basics_Status;
using SchoolProject.Data.Results;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.Models
{
    public class RegisterUserCommand : IRequest<Response<JwtAuthResult>>
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
