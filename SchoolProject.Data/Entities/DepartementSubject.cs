using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities
{
    public class DepartementSubject
    {
        [Key]
        public int? DID { get; set; }


        [Key]
        public int? SubID { get; set; }


        [ForeignKey("SubID")]
        [InverseProperty("DepartementSubjects")]
        public virtual Subject? Subject { get; set; }


        [ForeignKey("DID")]
        [InverseProperty("DepartementSubjects")]
        public virtual Departement? Departement { get; set; }
    }
}
