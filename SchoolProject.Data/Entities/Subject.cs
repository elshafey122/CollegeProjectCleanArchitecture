using SchoolProject.Data.CommonsLocalize;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities
{
    public class Subject : GenericLocalizableEntity
    {
        public Subject()
        {
            DepartementSubjects = new HashSet<DepartementSubject>();
            StudentSubjects = new HashSet<StudentSubject>();
            InstructorSubjects = new HashSet<InstructorSubject>();
        }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubID { get; set; }


        public string? SubNameAr { get; set; }


        public string? SubNameEn { get; set; }


        public int? Period { get; set; }


        [InverseProperty("Subject")]
        public virtual ICollection<DepartementSubject> DepartementSubjects { get; set; }


        [InverseProperty("Subject")]
        public virtual ICollection<StudentSubject> StudentSubjects { get; set; }


        [InverseProperty("Subject")]
        public virtual ICollection<InstructorSubject> InstructorSubjects { get; set; }

    }
}
