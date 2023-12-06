using SchoolProject.Data.Entities.Functions;
using System.Data.Common;

namespace SchoolProject.Infrustructure.IRepositories.IFunctions
{
    public interface IInstructorFunctionRepository
    {
        public decimal GetSummationSalaryOfInstructors(string query, DbCommand cmd);
        public Task<List<InstructorSalaryData>> GetInstructorsSalaryData(string query, DbCommand cmd);
    }
}
