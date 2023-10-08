using SchoolProject.Data.Entities;
using SchoolProject.Data.Helpers;

namespace SchoolProject.Service.Abstractions
{
    public interface IStudentService
    {
        public Task<List<Student>> GetStudentsListAsync();
        public Task<Student> GetStudentByIdWithIncludeAsync(int id);
        public Task<Student> GetById(int id);
        public Task<string> AddStudent(Student student);
        public Task<string> EditStudent(Student student);
        public Task<string> DeleteStudent(Student student);
        public IQueryable<Student> GetStudentspaginationQuerable();
        public IQueryable<Student> GetStudentListByDepartementId(int id);
        public Task<bool> IsDepartementIdExist(int id);
        public IQueryable<Student> FilterStudentPaginationQuerable(StudentOrderingEnum orderingnum, string search);
    }
}
