using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservationsManager.Domain.Models;

namespace EFCoreMappingApp.Configurations
{
    public class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(x => x.Description)
                .HasMaxLength(300)
                /*.IsRequired()*/;

            builder.Property(e => e.UserName)
                .HasMaxLength(50)
            .IsRequired();

            builder.Property(x => x.Email)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(50)
                /*.IsRequired()*/;

            builder.Property(x => x.PhoneNumber)
                .HasMaxLength(50)
                /*.IsRequired()*/;

            builder.HasMany(x => x.Actions).WithMany(x => x.Employees)
                .UsingEntity<ActionEmployee>();
        }
    }
}
