using SchoolProject.Data.Entities.Views;
using SchoolProject.Infrustructure.Data;
using SchoolProject.Infrustructure.InfrustructureBases;
using SchoolProject.Infrustructure.IRepositories.IViews;

namespace SchoolProject.Infrustructure.Repositories.Views
{
    public class ViewDeptRepository : GenericRepositories<ViewDepartStudentCount>, IViewRepository<ViewDepartStudentCount>
    {
        public ViewDeptRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
