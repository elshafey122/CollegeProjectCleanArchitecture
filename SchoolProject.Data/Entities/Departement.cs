using SchoolProject.Data.CommonsLocalize;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities
{
    public class Departement : GenericLocalizableEntity
    {
        public Departement()
        {
            Students = new HashSet<Student>();
            DepartementSubjects = new HashSet<DepartementSubject>();
            Instructors = new HashSet<Instructor>();
        }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DID { get; set; }


        [StringLength(500)]
        public string? DNameAr { get; set; }


        [StringLength(500)]
        public string? DNameEn { get; set; }


        public int? InsManager { get; set; }


        [InverseProperty("Departement")]
        public virtual ICollection<Student> Students { get; set; }


        [InverseProperty("Departement")]
        public virtual ICollection<DepartementSubject> DepartementSubjects { get; set; }


        [InverseProperty("Departement")]
        public virtual ICollection<Instructor> Instructors { get; set; }


        [ForeignKey(nameof(InsManager))]
        [InverseProperty("DepartementManager")]
        public virtual Instructor Instructor { get; set; }

    }
}
