using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Functions;
using SchoolProject.Infrustructure.Data;
using SchoolProject.Infrustructure.IRepositories;
using SchoolProject.Infrustructure.IRepositories.IFunctions;
using SchoolProject.Service.Abstractions;

namespace SchoolProject.Service.Implemintations
{
    public class InstructorService : IInstructorService
    {
        private readonly IInstructorRepository _instructorRepository;
        private readonly IInstructorFunctionRepository _instructorFunctionRepository;
        private readonly ApplicationDbContext _context;
        public InstructorService(IInstructorRepository instructorRepository, IInstructorFunctionRepository instructorFunctionRepository,
            ApplicationDbContext context)
        {
            _instructorRepository = instructorRepository;
            _context = context;
            _instructorFunctionRepository = instructorFunctionRepository;
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

        public async Task<List<InstructorSalaryData>> GetInstructorsSalaryData()
        {
            using (var cmd = _context.Database.GetDbConnection().CreateCommand()) // code for connect to database
            {
                if (cmd.Connection.State != System.Data.ConnectionState.Open) // check connection of database
                {
                    cmd.Connection.Open();
                }
                return await _instructorFunctionRepository.GetInstructorsSalaryData("select * from dbo.InstructorSalaryData()", cmd);
            }
        }

        public Task<decimal> GetSummationSalaryOfInstructors()
        {
            decimal result = 0;
            using (var cmd = _context.Database.GetDbConnection().CreateCommand()) // code for connect to database
            {
                if (cmd.Connection.State != System.Data.ConnectionState.Open) // check connection of database
                {
                    cmd.Connection.Open();
                }
                result = _instructorFunctionRepository.GetSummationSalaryOfInstructors("select dbo.GetSalarySummation()", cmd);
            }
            return Task.FromResult(result);
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
