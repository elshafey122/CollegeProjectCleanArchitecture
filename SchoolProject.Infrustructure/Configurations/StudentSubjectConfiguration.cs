using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrustructure.Configurations
{
    public class StudentSubjectConfiguration : IEntityTypeConfiguration<StudentSubject>
    {
        public void Configure(EntityTypeBuilder<StudentSubject> builder)
        {

            builder.HasKey(x => new { x.StudID, x.SubID });
            builder.HasOne(x => x.Student).WithMany(x => x.StudentSubjects)
                                                 .HasForeignKey(x => x.StudID)
                                                 .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Subject).WithMany(x => x.StudentSubjects)
                                                 .HasForeignKey(x => x.SubID)
                                                 .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
