using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrustructure.Configurations
{
    public class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
    {
        public void Configure(EntityTypeBuilder<Instructor> builder)
        {
            builder.HasOne(x => x.DepartementManager)
                                             .WithOne(x => x.Instructor)
                                             .HasForeignKey<Departement>(x => x.InsManager)
                                             .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.supervisor)
                                             .WithMany(x => x.Instructors)
                                             .HasForeignKey(x => x.SupervisorId)
                                             .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
