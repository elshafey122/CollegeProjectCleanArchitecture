using Microsoft.AspNetCore.Http;
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
        private readonly IFileService _fileService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;
        public InstructorService(IInstructorRepository instructorRepository, IInstructorFunctionRepository instructorFunctionRepository,
            ApplicationDbContext context, IFileService fileService, IHttpContextAccessor httpContextAccessor)
        {
            _instructorRepository = instructorRepository;
            _context = context;
            _instructorFunctionRepository = instructorFunctionRepository;
            _fileService = fileService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> AddInstructor(Instructor instructor, IFormFile file, string location)
        {
            var context = _httpContextAccessor.HttpContext.Request;
            var BaseUrl = context.Scheme + "://" + context.Host;
            var imageUrl = await _fileService.UploadImage(location, file);

            switch (imageUrl)
            {
                case "NoImage":
                    return "NoImage";
                case "FailedToUploadImage":
                    return "FailedToUploadImage";
            }
            var finalImageUrl = BaseUrl + imageUrl;
            instructor.Image = finalImageUrl;
            try
            {
                await _instructorRepository.AddAsync(instructor);
                return "Success";
            }
            catch (Exception)
            {
                return "FailedInAdd";
            }
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

        public async Task<string> EditInstructor(Instructor instructor, IFormFile file, string location)
        {
            var context = _httpContextAccessor.HttpContext.Request;
            var BaseUrl = context.Scheme + "://" + context.Host;
            var imageUrl = await _fileService.UploadImage(location, file);
            switch (imageUrl)
            {
                case "NoImage":
                    return "NoImage";
                case "FailedToUploadImage":
                    return "FailedToUploadImage";
            }
            var finalImageUrl = BaseUrl + imageUrl;
            instructor.Image = finalImageUrl;
            try
            {
                await _instructorRepository.UpdateAsync(instructor);
                return "Success";
            }
            catch (Exception)
            {
                return "FailedInAdd";
            }
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
            var result = _instructorFunctionRepository.GetSummationSalaryOfInstructors("select dbo.GetSalarySummation()");
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

        public async Task<bool> IsInstructorNameEnIsExist(string nameEn)
        {
            var res = await _instructorRepository.GetTableNoTracking().FirstOrDefaultAsync(x => x.NameEn == nameEn);
            if (res != null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> IsInstructorNameArIsExist(string nameAr)
        {
            var res = await _instructorRepository.GetTableNoTracking().FirstOrDefaultAsync(x => x.NameAr == nameAr);
            if (res != null)
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
