using BigBall.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BigBall.Context.Contracts.Configuration.Configurations
{
    /// <summary>
    /// Конфигурация <<see cref="Payment"/>
    /// </summary>
    internal class PaymentEntityTypeConfiguration : IEntityTypeConfiguration<Payment>
    {
        void IEntityTypeConfiguration<Payment>.Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.Title).HasMaxLength(20).IsRequired();
            builder.HasIndex(x => x.Title)
                .HasDatabaseName($"{nameof(Payment)}_{nameof(Payment.Title)}")
                .HasFilter($"{nameof(Payment.DeletedAt)} is null");

            builder.Property(x => x.CardNumber).HasMaxLength(17).IsRequired();
            builder.Property(x => x.Requisites).HasMaxLength(21).IsRequired();

            builder.HasIndex(x => x.CardNumber)
                .IsUnique()
                .HasDatabaseName($"{nameof(Payment)}_{nameof(Payment.CardNumber)}")
                .HasFilter($"{nameof(Payment.DeletedAt)} is null");

            builder.HasIndex(x => x.Title)
               .IsUnique()
               .HasDatabaseName($"{nameof(Payment)}_{nameof(Payment.Title)}")
               .HasFilter($"{nameof(Payment.DeletedAt)} is null");

            builder.HasMany(x => x.Reservations).WithOne(x => x.Payment).HasForeignKey(x => x.PaymentId);
        }
    }
}
