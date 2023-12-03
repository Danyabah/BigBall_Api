using BigBall.Common.Entity.DBInterfaces;
using BigBall.Context.Contracts;
using BigBall.Context.Contracts.Configuration.Configurations;
using BigBall.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;

namespace BigBall.Context
{
    /// <summary>
    /// Контекст работы с БД
    /// </summary>
    /// <remarks>
    /// 1) dotnet tool install --global dotnet -ef --version 6.0.0
    /// 2) dotnet tool update --global dotnet-ef
    /// 3) dotnet ef migrations add [name] --project BigBall.Context\BigBall.Context.csproj
    /// 4) dotnet ef database update --project BigBall.Context\BigBall.Context.csproj
    /// 5) dotnet ef database update [targetMigrationName] --BigBall.Context\BigBall.Context.csproj
    /// </remarks>
    public class BigBallContext : DbContext,
        IBigBallContext,
        IDbWriter,
        IDbRead,
        IUnitOfWork
    {
        public DbSet<Institution> Institutions { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<Person> People { get; set; }

        public DbSet<Promotion> Promotions { get; set; }

        public DbSet<Track> Tracks  { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        public BigBallContext(DbContextOptions<BigBallContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PromotionEntityTypeConfiguration).Assembly);
        }

        /// <summary>
        /// Сохранение изменений в БД
        /// </summary>
        async Task<int> IUnitOfWork.SaveChangesAsync(CancellationToken cancellationToken)
        {
            var count = await base.SaveChangesAsync(cancellationToken);
            foreach (var entry in base.ChangeTracker.Entries().ToArray())
            {
                entry.State = EntityState.Detached;
            }
            return count;
        }

        /// <summary>
        /// Чтение сущностей из БД
        /// </summary>
        IQueryable<TEntity> IDbRead.Read<TEntity>()
            => base.Set<TEntity>()
                .AsNoTracking()
                .AsQueryable();

        /// <summary>
        /// Запись сущности в БД
        /// </summary>
        void IDbWriter.Add<TEntity>(TEntity entity)
            => base.Entry(entity).State = EntityState.Added;

        /// <summary>
        /// Обновление сущностей
        /// </summary>
        void IDbWriter.Update<TEntity>(TEntity entity)
            => base.Entry(entity).State = EntityState.Modified;

        /// <summary>
        /// Удаление сущности из БД
        /// </summary>
        void IDbWriter.Delete<TEntity>(TEntity entity)
            => base.Entry(entity).State = EntityState.Deleted;
    }
}
