using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Enums;
using SchoolProject.Infrustructure.IRepositories;
using SchoolProject.Service.Abstractions;

namespace SchoolProject.Service.Implemintations
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentrepository;
        private readonly IDepartementRepository _departementRepository;

        public StudentService(IStudentRepository studentrepository, IDepartementRepository departementRepository)
        {
            _studentrepository = studentrepository;
            _departementRepository = departementRepository;
        }

        public async Task<string> AddStudent(Student student)
        {
            var checkstudname = _studentrepository.GetTableNoTracking().Where(x => x.StuNameEn.Equals(student.StuNameEn)).FirstOrDefault();
            if (checkstudname != null)
            {
                return "Exist";
            }
            await _studentrepository.AddAsync(student);
            return "Success";
        }

        public async Task<string> DeleteStudent(Student student)
        {
            var trans = _studentrepository.BeginTransaction();
            try
            {
                await _studentrepository.DeleteAsync(student);
                await trans.CommitAsync();
                return "Success";
            }
            catch
            {
                await trans.RollbackAsync();
                return "Failure";
            }
        }

        public async Task<string> EditStudent(Student student)
        {
            await _studentrepository.UpdateAsync(student);
            return "Success";
        }



        public IQueryable<Student> FilterStudentPaginationQuerable(StudentOrderingEnum orderingenum, string search)
        {
            var quarable = _studentrepository.GetTableNoTracking().Include(e => e.Departement).AsQueryable();
            if (search != null)
            {
                quarable = quarable.Where(x => x.StuNameEn.Contains(search) || x.Address.Contains(search));
            }
            switch (orderingenum)
            {
                case StudentOrderingEnum.StuId:
                    quarable = quarable.OrderBy(x => x.StuId);
                    break;
                case StudentOrderingEnum.StuName:
                    quarable = quarable.OrderBy(x => x.StuNameEn);
                    break;
                case StudentOrderingEnum.Address:
                    quarable = quarable.OrderBy(x => x.Address);
                    break;
                case StudentOrderingEnum.DepartementName:
                    quarable = quarable.OrderBy(x => x.Departement.DNameEn);
                    break;
                default:
                    quarable = quarable.OrderBy(x => x.StuId);
                    break;
            }
            return quarable;
        }

        public Task<Student> GetById(int id)
        {
            var student = _studentrepository.GetByIdAsync(id);
            return student;
        }

        public async Task<Student> GetStudentByIdWithIncludeAsync(int id)
        {
            var student = _studentrepository.GetTableNoTracking().Include(x => x.Departement)
                                            .Where(x => x.StuId == id).FirstOrDefault();
            return student;
        }

        public IQueryable<Student> GetStudentListByDepartementId(int id)
        {
            var students = _studentrepository.GetTableNoTracking().Where(x => x.DID == id).AsQueryable();
            return students;
        }

        public async Task<List<Student>> GetStudentsListAsync()
        {
            return await _studentrepository.GetStudentsListAsync();
        }

        public IQueryable<Student> GetStudentspaginationQuerable()
        {
            return _studentrepository.GetTableNoTracking().Include(e => e.Departement).AsQueryable();
        }

        public async Task<bool> IsDepartementIdExist(int id)
        {
            var result = await _departementRepository.GetTableNoTracking().AnyAsync(x => x.DID == id);
            return result;
        }
    }
}
