using BigBall.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;

namespace BigBall.Context.Contracts.Configuration.Configurations
{
    /// <summary>
    /// Конфигурация <<see cref="Track"/>
    /// </summary>
    internal class TrackEntityTypeConfiguration : IEntityTypeConfiguration<Track>
    {
        void IEntityTypeConfiguration<Track>.Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Track> builder)
        {
            builder.ToTable("Tracks");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.Number).IsRequired();
            builder.HasIndex(x => x.Number)
              .IsUnique()
              .HasDatabaseName($"{nameof(Track)}_{nameof(Track.Number)}")
              .HasFilter($"{nameof(Track.DeletedAt)} is null");

            builder.Property(x => x.Type).IsRequired();
            builder.HasIndex(x => x.Type).HasDatabaseName($"{nameof(Track)}_{nameof(Track.Type)}")
                 .HasFilter($"{nameof(Track.DeletedAt)} is null");

            builder.Property(x => x.Capacity).IsRequired();
            builder.HasMany(x => x.Reservations).WithOne(x => x.Track).HasForeignKey(x => x.TrackId);
        }
    }
}
