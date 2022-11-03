using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservationsManager.Domain;

namespace EFCoreMappingApp.Configurations
{
    public class TimeBlockConfig : IEntityTypeConfiguration<TimeBlock>
    {
        public void Configure(EntityTypeBuilder<TimeBlock> builder)
        {
            builder.Property(x => x.StartTime)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
