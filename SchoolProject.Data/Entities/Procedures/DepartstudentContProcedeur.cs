using SchoolProject.Data.CommonsLocalize;

namespace SchoolProject.Data.Entities.Procedures
{
    public class DepartstudentContProcedeur : GenericLocalizableEntity
    {
        public int DId { get; set; }
        public string DNameAr { get; set; }
        public string DNameEn { get; set; }
        public int StudentsCount { get; set; }
    }

    public class DepartstudentContProcedeurParams
    {
        public int DId { get; set; }
    }
}
