using SchoolProject.Data.Entities;

namespace SchoolProject.Service.Abstractions
{
    public interface IInstructorService
    {
        public IQueryable<Instructor> GetInstructorList();
        public Task<Instructor> GetInstructorbyId(int id);
        public Task<string> AddInstructor(Instructor instructor);
        public Task<string> EditInstructor(Instructor instructor);
        public Task<string> DeleteInstructor(int id);
        public Task<bool> IsInstructorIsExist(int? id);
        public Task<bool> IsInstructorNameEnExceptItselfIsExist(string? name, int? instructid);
        public Task<bool> IsInstructorNameArExceptItselfIsExist(string? name, int? instructid);


    }
}
