using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities
{
    public class InstructorSubject
    {
        [Key]
        public int InsId { get; set; }


        [Key]
        public int SubId { get; set; }


        [ForeignKey(nameof(InsId))]
        [InverseProperty("InstructorSubjects")]
        public virtual Instructor? Instructor { get; set; }


        [ForeignKey(nameof(SubId))]
        [InverseProperty("InstructorSubjects")]
        public virtual Subject? Subject { get; set; }


    }
}
