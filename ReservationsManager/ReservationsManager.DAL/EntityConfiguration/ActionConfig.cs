using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservationsManager.Domain.Auth;
using Action = ReservationsManager.Domain.Models.Action;

namespace EFCoreMappingApp.Configurations
{
    public class ActionConfig : IEntityTypeConfiguration<Action>
    {
        public void Configure(EntityTypeBuilder<Action> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();
        }
    }

    public class UserCredentialConfig : IEntityTypeConfiguration<UserCredential>
    {
        public void Configure(EntityTypeBuilder<UserCredential> builder)
        {
            builder.Property(x => x.Login)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Password)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
