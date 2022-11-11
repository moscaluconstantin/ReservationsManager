using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservationsManager.Domain.Models;

namespace EFCoreMappingApp.Configurations
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.UserName)
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
        }
    }
}
