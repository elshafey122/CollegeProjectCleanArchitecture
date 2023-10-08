using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using System.Reflection;

namespace SchoolProject.Infrustructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Departement> departements { get; set; }
        public DbSet<Student> students { get; set; }
        public DbSet<Subject> subjects { get; set; }
        public DbSet<Instructor> instructors { get; set; }
        public DbSet<DepartementSubject> departementSubjects { get; set; }
        public DbSet<StudentSubject> studentSubjects { get; set; }
        public DbSet<InstructorSubject> instructorSubjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
