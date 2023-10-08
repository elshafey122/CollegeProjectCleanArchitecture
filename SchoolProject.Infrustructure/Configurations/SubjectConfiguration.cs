using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrustructure.Configurations
{
    public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {

            builder.Property(x => x.SubNameAr).HasMaxLength(100);
            builder.Property(x => x.SubNameEn).HasMaxLength(100);
        }
    }
}
