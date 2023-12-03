using BigBall.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BigBall.Context.Contracts.Configuration.Configurations
{
    /// <summary>
    /// Конфигурация <<see cref="Reservation"/>
    /// </summary>
    internal class ReservationEntityTypeConfiguration : IEntityTypeConfiguration<Reservation>
    {
        void IEntityTypeConfiguration<Reservation>.Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.ToTable("Reservations");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.Date).IsRequired();
            builder.HasIndex(x => x.Date).HasDatabaseName($"{nameof(Reservation)}_{nameof(Reservation.Date)}")
                .HasFilter($"{nameof(Reservation.DeletedAt)} is null");

            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(200);
            builder.Property(x => x.PaymentId).IsRequired();
            builder.Property(x => x.InstitutionId).IsRequired();
            builder.Property(x => x.TrackId).IsRequired();
            builder.Property(x => x.PersonId).IsRequired();
        }
    }
}
