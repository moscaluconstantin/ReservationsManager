using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservationsManager.Domain;

namespace EFCoreMappingApp.Configurations
{
    public class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.UserName)
                .HasMaxLength(50)
            .IsRequired();

            builder.HasMany(x => x.Actions).WithMany(x => x.Employees)
                .UsingEntity<ActionEmployee>();
        }
    }
}
