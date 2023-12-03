using BigBall.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BigBall.Context.Contracts.Configuration.Configurations
{
    /// <summary>
    /// Конфигурация <<see cref="Person"/>
    /// </summary>
    internal class PersonEntityTypeConfiguration : IEntityTypeConfiguration<Person>
    {
        void IEntityTypeConfiguration<Person>.Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Persons");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.FirstName).HasMaxLength(40).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Patronymic).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(100).IsRequired();
            builder.HasIndex(x => x.Email)
                .IsUnique()
                .HasDatabaseName($"{nameof(Person)}_{nameof(Person.Email)}")
                .HasFilter($"{nameof(Person.DeletedAt)} is null");
            builder.Property(x => x.Age).IsRequired();
            builder.HasMany(x => x.Reservations).WithOne(x => x.Person).HasForeignKey(x => x.PersonId);
        }
    }
}
