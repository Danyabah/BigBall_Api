using BigBall.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BigBall.Context.Contracts.Configuration.Configurations
{
    /// <summary>
    /// Конфигурация <<see cref="Institution"/>
    /// </summary>
    internal class InstitutionEntityTypeConfiguration : IEntityTypeConfiguration<Institution>
    {
        void IEntityTypeConfiguration<Institution>.Configure(EntityTypeBuilder<Institution> builder)
        {
            builder.ToTable("Institutions");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.Title).HasMaxLength(20).IsRequired();
            builder.Property(x => x.Address).HasMaxLength(150).IsRequired();
            builder.HasIndex(x => x.Address)
                .IsUnique()
                .HasDatabaseName($"{nameof(Institution)}_{nameof(Institution.Address)}")
                .HasFilter($"{nameof(Institution.DeletedAt)} is null");
            builder.Property(x => x.ClosingTime).HasMaxLength(8).IsRequired();
            builder.Property(x => x.OpeningTime).HasMaxLength(8).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(30);
            builder.HasMany(x => x.Reservations).WithOne(x => x.Institution).HasForeignKey(x => x.InstitutionId);
        }
    }
}
