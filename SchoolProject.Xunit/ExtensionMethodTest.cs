﻿using CollegeProject.XUnitTest.Wrappers;
using EntityFrameworkCore.Testing.Common;
using FluentAssertions;
using Moq;
using SchoolProject.Core.Wrappings;
using SchoolProject.Data.Entities;

namespace CollegeProject.XUnitTest
{
    public class ExtensionMethodTest
    {
        private readonly Mock<IPaginatedService<Student>> _paginatedServiceMock;
        public ExtensionMethodTest()
        {
            _paginatedServiceMock = new();
        }

        [Theory]
        [InlineData(1, 10)]
        public async Task ToPaginatedListAsync_Should_Return_List(int pageNumber, int pageSize)
        {
            //Arrange
            var department = new Departement() { DID = 1, DNameAr = "هندسة البرمجيات", DNameEn = "SE" };

            var studentList = new AsyncEnumerable<Student>(new List<Student>
            {
                new Student(){ StuId=1, Address="Alex", DID=1, StuNameAr="محمد",StuNameEn="mohamed",Departement=department}
            });
            var paginatedResult = new PaginatedResult<Student>(true,studentList.ToList());
            _paginatedServiceMock.Setup(x => x.ReturnPaginatedResult(studentList, pageNumber, pageSize)).Returns(Task.FromResult(paginatedResult));
            //Act
            var result = await _paginatedServiceMock.Object.ReturnPaginatedResult(studentList, pageNumber, pageSize);
            //Assert
            result.Data.Should().NotBeNullOrEmpty();
        }
    }
}
