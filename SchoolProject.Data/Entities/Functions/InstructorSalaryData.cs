using SchoolProject.Data.CommonsLocalize;

namespace SchoolProject.Data.Entities.Functions
{
    public class InstructorSalaryData : GenericLocalizableEntity
    {
        public int DId { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public decimal Salary { get; set; }
    }
}
