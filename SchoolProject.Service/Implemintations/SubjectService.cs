using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Enums;
using SchoolProject.Infrustructure.IRepositories;
using SchoolProject.Service.Abstractions;

namespace SchoolProject.Service.Implemintations
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;
        public SubjectService(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<string> AddSubjectAsync(Subject subject)
        {
            try
            {
                var result = await _subjectRepository.AddAsync(subject);
                return "Success";
            }
            catch (Exception)
            {
                return "FailedToAdd";
            }
        }

        public async Task<string> DeleteSubjectAsync(int subjectId)
        {
            var SelectedSubject = await _subjectRepository.GetByIdAsync(subjectId);
            if (SelectedSubject == null)
                return "NotFound";
            try
            {
                await _subjectRepository.DeleteAsync(SelectedSubject);
                return "Success";
            }
            catch
            {
                return "FailedToDelete";
            }
        }

        public async Task<string> EditSubjectAsync(Subject subject)
        {
            var SelectedSubject = await _subjectRepository.GetTableNoTracking().FirstOrDefaultAsync(x => x.SubID == subject.SubID);
            if (SelectedSubject == null)
                return "NotFound";
            try
            {
                await _subjectRepository.UpdateAsync(subject);
                return "Success";
            }
            catch
            {
                return "FailedToUpdate";
            }
        }

        public IQueryable<Subject> FilterSubjectsPaginationQuerable(SubjectsOrderingEnum orderBy, string search)
        {
            var subjects = _subjectRepository.GetTableNoTracking().AsQueryable();
            if (search != null)
            {
                subjects = subjects.Where(x => x.SubNameEn.Contains(search) || x.SubNameAr.Contains(search));
            }
            switch (orderBy)
            {
                case SubjectsOrderingEnum.SubjectID:
                    subjects = subjects.OrderBy(x => x.SubID);
                    break;
                case SubjectsOrderingEnum.SubNameAr:
                    subjects = subjects.OrderBy(x => x.SubNameAr);
                    break;
                case SubjectsOrderingEnum.SubNameEn:
                    subjects = subjects.OrderBy(x => x.SubNameEn);
                    break;
                case SubjectsOrderingEnum.Period:
                    subjects = subjects.OrderBy(x => x.Period);
                    break;
                default:
                    subjects = subjects.OrderBy(x => x.SubID);
                    break;
            }
            return subjects;
        }

        public async Task<Subject> GetSubjectById(int subjectId)
        {
            return await _subjectRepository.GetByIdAsync(subjectId);
        }

        public async Task<bool> IsSubjectNameArIsExist(string nameAr)
        {
            var result = await _subjectRepository.GetTableNoTracking().FirstOrDefaultAsync(x => x.SubNameAr == nameAr);
            if (result != null)
                return false;
            return true;

        }
        public async Task<bool> IsSubjectNameEnIsExist(string nameEn)
        {
            var result = await _subjectRepository.GetTableNoTracking().FirstOrDefaultAsync(x => x.SubNameEn == nameEn);
            if (result != null)
                return false;
            return true;
        }
    }
}
