using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrustructure.Configurations
{
    public class DepartementSubjectConfiguration : IEntityTypeConfiguration<DepartementSubject>
    {
        public void Configure(EntityTypeBuilder<DepartementSubject> builder)
        {

            builder.HasKey(x => new { x.DID, x.SubID });
            builder.HasOne(x => x.Departement).WithMany(x => x.DepartementSubjects)
                                                 .HasForeignKey(x => x.DID)
                                                 .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Subject).WithMany(x => x.DepartementSubjects)
                                                 .HasForeignKey(x => x.SubID)
                                                 .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
