using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservationsManager.Domain.Models;

namespace EFCoreMappingApp.Configurations
{
    public class ActionEmployeeConfig : IEntityTypeConfiguration<ActionEmployee>
    {
        public void Configure(EntityTypeBuilder<ActionEmployee> builder)
        {
            builder.HasOne(x => x.Employee)
                .WithMany()
                .HasForeignKey(x => x.EmployeeID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Action)
                .WithMany()
                .HasForeignKey(x => x.ActionID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
