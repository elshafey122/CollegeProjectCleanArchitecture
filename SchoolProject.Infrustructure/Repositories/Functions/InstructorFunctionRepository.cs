using SchoolProject.Data.Entities.Functions;
using SchoolProject.Infrustructure.IRepositories.IFunctions;
using StoredProcedureEFCore;
using System.Data.Common;

namespace SchoolProject.Infrustructure.Repositories.Functions
{
    public class InstructorFunctionRepository : IInstructorFunctionRepository
    {
        public decimal GetSummationSalaryOfInstructors(string query, DbCommand cmd)
        {
            decimal response = 0;
            cmd.CommandText = query;
            var value = cmd.ExecuteScalar();
            var result = value.ToString();
            if (decimal.TryParse(result, out decimal d))
            {
                response = d;
            }
            return response;
        }

        public async Task<List<InstructorSalaryData>> GetInstructorsSalaryData(string query, DbCommand cmd)
        {
            cmd.CommandText = query;
            var reader = await cmd.ExecuteReaderAsync();
            var result = await reader.ToListAsync<InstructorSalaryData>();
            return result;
        }
    }
}
