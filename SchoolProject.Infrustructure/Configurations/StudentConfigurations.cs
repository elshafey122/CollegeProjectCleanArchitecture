using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrustructure.Configurations
{
    public class StudentConfigurations : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasOne(x => x.Departement)
                                         .WithMany(x => x.Students)
                                         .HasForeignKey(x => x.DID)
                                         .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
