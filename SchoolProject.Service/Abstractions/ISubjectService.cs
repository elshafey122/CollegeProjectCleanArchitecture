using SchoolProject.Data.Entities;
using SchoolProject.Data.Enums;

namespace SchoolProject.Service.Abstractions
{
    public interface ISubjectService
    {
        public IQueryable<Subject> FilterSubjectsPaginationQuerable(SubjectsOrderingEnum orderBy, string search);
        public Task<Subject> GetSubjectById(int subjectId);
        public Task<string> AddSubjectAsync(Subject subject);
        public Task<string> EditSubjectAsync(Subject subject);
        public Task<string> DeleteSubjectAsync(int subjectId);
        public Task<bool> IsSubjectNameArIsExist(string nameAr);
        public Task<bool> IsSubjectNameEnIsExist(string nameEn);
    }
}
