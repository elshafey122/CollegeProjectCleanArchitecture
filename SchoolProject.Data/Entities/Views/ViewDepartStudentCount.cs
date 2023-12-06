using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.CommonsLocalize;

namespace SchoolProject.Data.Entities.Views
{
    [Keyless]
    public class ViewDepartStudentCount : GenericLocalizableEntity
    {
        public int DId { get; set; }
        public string DNameAr { get; set; }
        public string DNameEn { get; set; }
        public int StudentsCount { get; set; }
    }
}
