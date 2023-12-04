using BigBall.Common.Entity.DBInterfaces;
using BigBall.Common.Entity;

namespace BigBall.API.Extensions
{
    /// <summary>
    /// Реализация <see cref="IDbWriterContext"/>
    /// </summary>
    public class DbWriterContext : IDbWriterContext
    {
        /// <inheritdoc/>
        public IDbWriter Writer { get; }

        /// <inheritdoc/>
        public IUnitOfWork UnitOfWork { get; }

        /// <inheritdoc/>
        public IDateTimeProvider DateTimeProvider { get; }

        /// <inheritdoc/>
        public string UserName { get; } = "BigBall.Api";

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="DbWriterContext"/>
        /// </summary>
        /// <remarks>В реальном проекте надо добавлять IIdentity для доступа к
        /// информации об авторизации</remarks>
        public DbWriterContext(
            IDbWriter writer,
            IUnitOfWork unitOfWork, IDateTimeProvider provider)
        {
            Writer = writer;
            UnitOfWork = unitOfWork;
            DateTimeProvider = provider;
        }
    }
}
