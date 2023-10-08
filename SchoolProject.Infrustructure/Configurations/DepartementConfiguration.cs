using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrustructure.Configurations
{
    public class DepartementConfiguration : IEntityTypeConfiguration<Departement>
    {
        public void Configure(EntityTypeBuilder<Departement> builder)
        {
            builder.HasMany(x => x.Instructors)
                                          .WithOne(x => x.Departement)
                                          .HasForeignKey(x => x.DId)
                                          .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
