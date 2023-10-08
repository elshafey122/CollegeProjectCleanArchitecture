using SchoolProject.Data.CommonsLocalize;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities
{
    public class Instructor : GenericLocalizableEntity
    {
        public Instructor()
        {
            Instructors = new HashSet<Instructor>();
            InstructorSubjects = new HashSet<InstructorSubject>();
        }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InsId { get; set; }


        public string? NameAr { get; set; }


        public string? NameEn { get; set; }


        public string? Position { get; set; }


        public string? Address { get; set; }


        public decimal? Salary { get; set; }


        public int? SupervisorId { get; set; }


        public int? DId { get; set; }


        [ForeignKey(nameof(DId))]
        [InverseProperty("Instructors")]
        public Departement? Departement { get; set; }


        [InverseProperty("Instructor")]
        public Departement DepartementManager { get; set; }


        [ForeignKey(nameof(SupervisorId))]
        [InverseProperty("Instructors")]
        public Instructor? supervisor { get; set; }


        [InverseProperty("supervisor")]
        public ICollection<Instructor> Instructors { get; set; }


        [InverseProperty("Instructor")]
        public ICollection<InstructorSubject> InstructorSubjects { get; set; }
    }
}
