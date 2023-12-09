using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities.Functions;
using SchoolProject.Infrustructure.Data;
using SchoolProject.Infrustructure.IRepositories.IFunctions;
using StoredProcedureEFCore;
using System.Data.Common;

namespace SchoolProject.Infrustructure.Repositories.Functions
{
    public class InstructorFunctionRepository : IInstructorFunctionRepository
    {
        private readonly ApplicationDbContext _context;
        public InstructorFunctionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public decimal GetSummationSalaryOfInstructors(string query)
        {
            using (var cmd = _context.Database.GetDbConnection().CreateCommand()) // code for connect to database
            {
                if (cmd.Connection.State != System.Data.ConnectionState.Open) // check connection of database
                {
                    cmd.Connection.Open();
                }
                decimal response = 0;
                cmd.CommandText = query;
                var value = cmd.ExecuteScalar();
                var result = value.ToString();
                if (decimal.TryParse(result, out decimal d))
                {
                    response = d;
                }
                cmd.Connection.Close();
                return response;
            }
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
