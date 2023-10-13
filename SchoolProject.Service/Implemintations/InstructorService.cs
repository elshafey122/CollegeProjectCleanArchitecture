using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrustructure.IRepositories;
using SchoolProject.Service.Abstractions;

namespace SchoolProject.Service.Implemintations
{
    public class InstructorService : IInstructorService
    {
        private readonly IInstructorRepository _instructorRepository;
        public InstructorService(IInstructorRepository instructorRepository)
        {
            _instructorRepository = instructorRepository;
        }

        public async Task<string> AddInstructor(Instructor instructor)
        {
            var result = await _instructorRepository.GetTableNoTracking().Where(x => x.NameAr.Equals(instructor.NameAr) || x.NameEn.Equals(instructor.NameEn)).FirstOrDefaultAsync();
            if (result != null)
            {
                return "Exist";
            }
            await _instructorRepository.AddAsync(instructor);
            return "Success";
        }

        public async Task<string> DeleteInstructor(int id)
        {
            var instructor = await _instructorRepository.GetByIdAsync(id);
            if (instructor == null)
            {
                return "NotFound";
            }
            var transaction = _instructorRepository.BeginTransaction();
            try
            {
                await _instructorRepository.DeleteAsync(instructor);
                transaction.Commit();
                return "Success";
            }
            catch
            {
                transaction.Rollback();
                return "Failure";
            }
        }

        public async Task<string> EditInstructor(Instructor instructor)
        {
            await _instructorRepository.UpdateAsync(instructor);
            return "Success";
        }

        public async Task<Instructor> GetInstructorbyId(int id)
        {
            var instructor = await _instructorRepository.GetTableNoTracking().Where(x => x.InsId.Equals(id))
                                                   .Include(x => x.Departement).Include(x => x.supervisor)
                                                   .Include(x => x.InstructorSubjects).FirstOrDefaultAsync();
            return instructor;
        }

        public IQueryable<Instructor> GetInstructorList()
        {
            var instructorslist = _instructorRepository.GetTableNoTracking().Include(x => x.Departement).AsQueryable();
            return instructorslist;
        }

        public async Task<bool> IsInstructorIsExist(int? id)
        {
            var res = await _instructorRepository.GetTableNoTracking().FirstOrDefaultAsync(x => x.InsId == id);
            if (res == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> IsInstructorNameArExceptItselfIsExist(string? name, int? id)
        {
            var result = await _instructorRepository.GetTableNoTracking().Where(x => x.NameAr == name && x.InsId != id).FirstOrDefaultAsync();
            if (result != null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> IsInstructorNameEnExceptItselfIsExist(string? name, int? id)
        {
            var result = await _instructorRepository.GetTableNoTracking().Where(x => x.NameEn == name && x.InsId != id).FirstOrDefaultAsync();
            if (result != null)
            {
                return false;
            }
            return true;
        }

    }
}
