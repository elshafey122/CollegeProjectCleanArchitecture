using AutoMapper;
using CollegeProject.XUnitTest.ClassDataAndMemberData;
using EntityFrameworkCore.Testing.Common;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Moq;
using SchoolProject.Core.Features.Students.Commands.Handlers;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Features.Students.Queries.Handlers;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Core.Features.Students.Queries.Results;
using SchoolProject.Core.Localization;
using SchoolProject.Core.Mapping.Students;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Enums;
using SchoolProject.Service.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CollegeProject.XUnitTest.CoreTest.Queires
{
    public class StudentHandlerCommandTest
    {
        private readonly Mock<IStringLocalizer<SharedResources>> _localzermock;
        private readonly Mock<IStudentService> _studentservicemock;
        private readonly IMapper _mappermock;
        private readonly StudentProfile studentProfile;

        public StudentHandlerCommandTest()
        {
            studentProfile = new();
            _localzermock = new();
            _studentservicemock = new();
            var configuration = new MapperConfiguration(c => c.AddProfile(studentProfile));
            _mappermock = new Mapper(configuration); // add all configuration of mapping of student into mapper
        }

        [Fact]
        public async Task Handle_Add_Student_ShouldAdd_AndReturnStatus_401()
        {
            // arrange
            var handle = new StudentHandlerCommand(_studentservicemock.Object,_mappermock,_localzermock.Object);
            var addstudentcommand = new AddStudentCommand() { StuNamear = "على", StuNameen = "ali" };
            _studentservicemock.Setup(x => x.AddStudent(It.IsAny<Student>())).Returns(Task.FromResult("Success")); // add any student
            
            // act
            var result =await handle.Handle(addstudentcommand, default);
            
            // assert
            result.Succeeded.Should().BeTrue();
            result.StatusCode.Should().Be(HttpStatusCode.Created);
            _studentservicemock.Verify(x => x.AddStudent(It.IsAny<Student>()), Times.Exactly(1), "not called true"); // true condition
        }

        [Fact]
        public async Task Handle_Add_Student_Shouldnotadd_AndReturnStatus_404()
        {
            // arrange
            var handle = new StudentHandlerCommand(_studentservicemock.Object, _mappermock, _localzermock.Object);
            var addstudentcommand = new AddStudentCommand() { StuNamear = "على", StuNameen = "ali" };
            _studentservicemock.Setup(x => x.AddStudent(It.IsAny<Student>())).Returns(Task.FromResult("")); // add any student

            // act
            var result = await handle.Handle(addstudentcommand, default);

            // assert
            result.Succeeded.Should().BeFalse();
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Handle_Edit_Student_Should_Return_Status_404()
        {
            // arrange
            var handle = new StudentHandlerCommand(_studentservicemock.Object, _mappermock, _localzermock.Object);
            var editStudentCommand = new EditStudentCommand() {Id=5, StuNamear = "على", StuNameen = "ali" };
            Student student = null;
            int resultx = 0;
            _studentservicemock.Setup(x => x.GetStudentByIdWithIncludeAsync(editStudentCommand.Id))
                               .Returns(Task.FromResult(student))
                               .Callback((int x)=>resultx=x); // add any student
            // act
            var result = await handle.Handle(editStudentCommand, default);

            // assert
            Assert.Equal(resultx,editStudentCommand.Id);
            result.Succeeded.Should().BeFalse();
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }


    }
    
}
