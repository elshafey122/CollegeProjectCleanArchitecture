using AutoMapper;
using CollegeProject.XUnitTest.ClassDataAndMemberData;
using EntityFrameworkCore.Testing.Common;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Moq;
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
    public class StudentHandlerQueryTest
    {
        private readonly Mock<IStringLocalizer<SharedResources>> _localzermock;
        private readonly Mock<IStudentService> _studentservicemock;
        private readonly IMapper _mappermock;
        private readonly StudentProfile studentProfile;

        public StudentHandlerQueryTest()
        {
            studentProfile = new();
            _localzermock = new();
            _studentservicemock = new(); 
            var configuration = new MapperConfiguration(c => c.AddProfile(studentProfile)); 
            _mappermock = new Mapper(configuration); // add all configuration of mapping of student into mapper
        }


        [Fact]
        public async Task Handle_StudentList_Should_NotNull_AndNotEmpty()
        {
            // Arrange
            var students = new List<Student>()
            {
                new Student()
                {
                    Address="tanta",
                    StuId=1,
                    StuNameAr="على",
                    StuNameEn="ali",
                    DID=1,
                    Phone="01043848393",
                }
            };
            var query = new GetStudentsListQuery(); 
            _studentservicemock.Setup(x => x.GetStudentsListAsync()).Returns(Task.FromResult(students)); // to get function from service
            var handler = new StudentHandlerQuery(_studentservicemock.Object, _mappermock,_localzermock.Object);

            //Act
            var result =await handler.Handle(query, default);


            // Assert
            result.Data.Should().NotBeNullOrEmpty();
            result.Succeeded.Should().BeTrue();
            result.Data.Should().BeOfType<List<StudentListResponse>>();
        }

        [Theory]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public async Task Handle_StudentByID_When_Is_Null_Should_Return_StatusCode404(int id)
        {

            // Arrange
            Departement departement = new()
            {
                DID = 1,
                DNameAr = "هندسة برمجيات",
                DNameEn = "Software engiineering",
                InsManager = null,
            };
            var students = new List<Student>()
            {
                new Student()
                {
                    Address="tanta",
                    StuId=1,
                    StuNameAr="على",
                    StuNameEn="ali",
                    DID=1,
                    Phone="01043848393",
                },
                new Student()
                {
                    Address="tanta",
                    StuId=2,
                    StuNameAr="طه",
                    StuNameEn="taha",
                    DID=1,
                    Phone="01043848393",
                }
            };
            var query = new GetStudentByIdQuery(id);
            _studentservicemock.Setup(x => x.GetStudentByIdWithIncludeAsync(id)).Returns(Task.FromResult(students.FirstOrDefault(x=>x.StuId==id))); 
            var handler = new StudentHandlerQuery(_studentservicemock.Object, _mappermock, _localzermock.Object);

            //Act
            var result = await handler.Handle(query, default);


            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Theory]
        //[InlineData(1)]
        //[InlineData(2)]
        //[ClassData(typeof(PassDatatoParamsUsingClassData))]
        [MemberData(nameof(PassDatatoParamsUsingMemberData.GetParamData),MemberType = typeof(PassDatatoParamsUsingMemberData))]
        public async Task Handle_StudentByID_When_return_Should_Return_StatusCode200(int id)
        {
            // Arrange
            Departement departement = new()
            {
                DID = 1,
                DNameAr = "هندسة برمجيات",
                DNameEn = "Software engiineering",
                InsManager = null,
            };
            var students = new List<Student>()
            {
                new Student()
                {
                    Address="tanta",
                    StuId=1,
                    StuNameAr="على",
                    StuNameEn="ali",
                    DID=1,
                    Phone="01043848393",
                },
                new Student()
                {
                    Address="tanta",
                    StuId=2,
                    StuNameAr="طه",
                    StuNameEn="taha",
                    DID=1,
                    Phone="01043848393",
                }
            };
            var query = new GetStudentByIdQuery(id);
            _studentservicemock.Setup(x => x.GetStudentByIdWithIncludeAsync(id)).Returns(Task.FromResult(students.FirstOrDefault(x => x.StuId == id)));
            var handler = new StudentHandlerQuery(_studentservicemock.Object, _mappermock, _localzermock.Object);

            //Act
            var result = await handler.Handle(query, default);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Handle_PaginatedStudentList_Should_NotNull_AndNotEmpty()
        {
            // Arrange
            Departement departement = new()
            {
                DID = 1,
                DNameAr = "هندسة برمجيات",
                DNameEn = "Software engiineering",
                InsManager = null,
            };

            var students = new AsyncEnumerable<Student>(new List<Student>
            {
               new Student()
               {
                    Address="tanta",
                    StuId=1,
                    StuNameAr="على",
                    StuNameEn="ali",
                    DID=1,
                    Phone="01043848393",
                    Departement = departement,
               } 
            });

            var query = new GetStudentPaginatedListQuery() { PageNumber =1 , PageSize = 10, Search= "ali", OrderBy = StudentOrderingEnum.StuId };
            _studentservicemock.Setup(x => x.FilterStudentPaginationQuerable(query.OrderBy,query.Search)).Returns(students.AsQueryable); // to get function from service
            var handler = new StudentHandlerQuery(_studentservicemock.Object, _mappermock, _localzermock.Object);

            //Act
            var result = await handler.Handle(query, default);

            
            // Assert
            result.Data.Should().NotBeNullOrEmpty();
            result.Data.Should().BeOfType<List<StudentListPaginatedResponse>>();
        }
        ahmed adjoisjfo = new ahmed();


    }
    
}
