using BigBall.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BigBall.Context.Contracts.Configuration.Configurations
{
    /// <summary>
    /// Конфигурация <<see cref="Promotion"/>
    /// </summary>
    public class PromotionEntityTypeConfiguration : IEntityTypeConfiguration<Promotion>
    {
        void IEntityTypeConfiguration<Promotion>.Configure(EntityTypeBuilder<Promotion> builder)
        {
            builder.ToTable("Promotions");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.PropertyAuditConfiguration();         
            builder.Property(x => x.Title).HasMaxLength(20).IsRequired();
            builder.HasIndex(x => x.Title)
                .IsUnique()
                .HasDatabaseName($"{nameof(Promotion)}_{nameof(Promotion.Title)}")
                .HasFilter($"{nameof(Promotion.DeletedAt)} is null");
            builder.Property(x => x.PercentageDiscount).IsRequired();
            builder.HasMany(x => x.Reservations).WithOne(x => x.Promotion).HasForeignKey(x => x.PromotionId);
        }
    }
}
