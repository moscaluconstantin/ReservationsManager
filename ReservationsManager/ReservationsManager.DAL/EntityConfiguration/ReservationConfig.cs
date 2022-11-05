using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservationsManager.Domain.Models;

namespace EFCoreMappingApp.Configurations
{
    public class ReservationConfig : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.ActionEmployee)
                .WithMany()
                .HasForeignKey(x => x.ActionEmployeeID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
