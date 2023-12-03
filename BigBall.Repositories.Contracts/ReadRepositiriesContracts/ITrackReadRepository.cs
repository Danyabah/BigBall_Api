using BigBall.Context.Contracts.Models;

namespace BigBall.Repositories.Contracts.ReadRepositiriesContracts
{
    /// <summary>
    /// Репозиторий чтения <see cref="Track"/>
    /// </summary>
    public interface ITrackReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Track"/>
        /// </summary>
        Task<IReadOnlyCollection<Track>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Track"/> по идентификатору
        /// </summary>
        Task<Track?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Track"/> по идентификаторам
        /// </summary>
        Task<Dictionary<Guid, Track>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);

        /// <summary>
        /// Проверить есть ли <see cref="Track"/> в коллекции
        /// </summary>
        Task<bool> IsNotNullAsync(Guid id, CancellationToken cancellationToken);
    }
}
