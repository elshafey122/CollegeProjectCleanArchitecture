using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrustructure.Configurations
{
    public class InstructorSubjectConfiguration : IEntityTypeConfiguration<InstructorSubject>
    {
        public void Configure(EntityTypeBuilder<InstructorSubject> builder)
        {
            builder.HasKey(x => new { x.InsId, x.SubId });
            builder.HasOne(x => x.Instructor).WithMany(x => x.InstructorSubjects)
                                                 .HasForeignKey(x => x.InsId)
                                                 .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Subject).WithMany(x => x.InstructorSubjects)
                                                 .HasForeignKey(x => x.SubId)
                                                 .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
