using SchoolProject.Data.CommonsLocalize;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities
{
    public class Student : GenericLocalizableEntity
    {
        public Student()
        {
            StudentSubjects = new HashSet<StudentSubject>();
        }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StuId { get; set; }


        [StringLength(200)]
        public string? StuNameAr { get; set; }


        [StringLength(200)]
        public string? StuNameEn { get; set; }


        [StringLength(500)]
        public string? Address { get; set; }


        [StringLength(500)]
        public string? Phone { get; set; }


        public int? DID { get; set; }


        [ForeignKey("DID")]
        [InverseProperty("Students")]
        public virtual Departement? Departement { get; set; }


        [InverseProperty("Student")]
        public virtual ICollection<StudentSubject> StudentSubjects { get; set; }
    }
}
